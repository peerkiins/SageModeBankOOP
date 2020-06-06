using System;

namespace SageModeBankOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempUsername = string.Empty;
            string tempPassword = string.Empty;
            Bank b = new Bank();
            b.Name = "Peerkiins";
            while (!b.IsLoggedIn())
            {
                Console.Clear();
                Console.WriteLine($"Welcome to {b.Name}");

                switch (ShowMenu("Register", "Login", "Exit"))
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("[Registration]");
                        Console.Write("Please enter your username: ");
                        tempUsername = Console.ReadLine();
                        if (b.IsAccountExist(tempUsername))
                        {
                            Console.WriteLine("Account already exist...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Write("Please enter your password: ");
                            tempPassword = Console.ReadLine();
                            b.Register(tempUsername, tempPassword);
                            Console.WriteLine("You have registered sucessfully!");
                            Console.ReadKey();
                        }
                        continue;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("[Login");
                        Console.Write("Please enter your Username: ");
                        tempUsername = Console.ReadLine();
                        Console.Write("Please enter your Password: ");
                        tempPassword = Console.ReadLine();
                        if (b.Login(tempUsername, tempPassword))
                        {
                            Console.WriteLine("Login Successful.");
                            Console.ReadKey();
                            break;
                        }
                        else if (!b.IsLoggedIn())
                        {
                            Console.Clear();
                            Console.WriteLine("Username and Password does not match.");
                            Console.ReadKey();
                        }
                        continue;
                    case '3':
                        return;
                    default:
                        break;
                }
            }
            while (b.IsLoggedIn())
            {
                Console.Clear();
                Console.Write($"Hi, {b.LoggedInAccount.Username}");
                Console.WriteLine($"your current balance is {b.LoggedInAccount.Balance}");
                switch (ShowMenu("Deposit", "Withdraw", "Transfer", "Transactions", "Logout"))
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("[Deposit]");
                        Console.Write("Enter deposit amount: ");
                        decimal DepAmount;
                        Decimal.TryParse(Console.ReadLine(), out DepAmount);
                        if (DepAmount < 0)
                        {
                            Console.WriteLine("Invalid Amount");
                            Console.ReadKey();
                        }
                        else
                        {
                            b.LoggedInAccount.Deposit(DepAmount);
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("[Withdraw]");
                        Console.Write("Enter amount: ");
                        decimal WithAmount;
                        if (Decimal.TryParse(Console.ReadLine(), out WithAmount))
                        {
                            if (b.LoggedInAccount.Balance < WithAmount && WithAmount < 0)
                            {
                                Console.WriteLine("Insufficient Funds or Invalid Amount");
                            }
                            else
                            {
                                b.LoggedInAccount.Withdraw(WithAmount);
                                Console.ReadKey();
                            }
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("[Transfer]");
                        Console.Write("Enter recipient: ");
                        string receiverAcc = Console.ReadLine();
                        Console.Write("Enter amount: ");
                        decimal TranAmount;
                        if (Decimal.TryParse(Console.ReadLine(), out TranAmount))
                        {
                            if (b.IsAccountExist(receiverAcc))
                            {
                                b.LoggedInAccount.Send(TranAmount);
                                b.Receive(receiverAcc, TranAmount);
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Recipient does not exist.");
                        }
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("[Transactions]");
                        break;
                    case '5':
                        b.ToLogout();
                        break;
                    default:
                        break;
                }
            }
        }
        static char ShowMenu(params string[] items)
        {
            string menuString = "Press ";
            for (int i = 0; i < items.Length; i++)
            {
                string postFix = i == items.Length - 1 ? string.Empty : ", ";
                menuString += $"{i + 1} to {items[i]}{postFix}";
            }
            Console.WriteLine($"\n{menuString}, ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            return key.KeyChar;
        }
    }
}
