using System;

namespace SageModeBankOOP
{
    class Program
    {
        static string tempUsername = string.Empty;
        static string tempPassword = string.Empty;
        static Bank b = new Bank();
        static void Main(string[] args)
        {
            b.Name = "Peerkiins";
            bool ShouldExit = false;
            while (!ShouldExit)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to {b.Name}");

                switch (ShowMenu("Register", "Login", "Exit"))
                {
                    case '1':
                        ShowRegister();
                        continue;
                    case '2':
                        ShowLogin();
                        continue;
                    case '3':
                        return;
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
        static void ShowRegister()
        {
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
        }
        static void ShowLogin()
        {
            Console.Clear();
            Console.WriteLine("[Login]");
            Console.Write("Please enter your Username: ");
            tempUsername = Console.ReadLine();
            Console.Write("Please enter your Password: ");
            tempPassword = Console.ReadLine();
            bool ShouldLogout = false;
            if (b.Login(tempUsername, tempPassword))
            {
                Console.WriteLine("Login Successful.");
                while (!ShouldLogout)
                {
                    Console.Clear();
                    Console.WriteLine($"Hi, {b.CurrentAccount.Username}");
                    Console.WriteLine($"your current balance is {b.CurrentAccount.Balance}");
                    switch (ShowMenu("Deposit", "Withdraw", "Transfer", "Transactions", "Logout"))
                    {
                        case '1':
                            ShowDeposit();
                            break;
                        case '2':
                            ShowWithdraw();
                            break;
                        case '3':
                            ShowTransfer();
                            break;
                        case '4':
                            ShowTransactions();
                            break;
                        case '5':
                            ShouldLogout = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Username and Password does not match.");
                Console.ReadKey();
            }
        }
        static void ShowDeposit()
        {
            Console.Clear();
            Console.WriteLine("[Deposit]");
            Console.Write("Enter deposit amount: ");
            decimal DepAmount;
            Decimal.TryParse(Console.ReadLine(), out DepAmount);
            if (DepAmount <= 0)
            {
                Console.WriteLine("Invalid Amount");
            }
            else
            {
                b.CurrentAccount.Deposit(DepAmount);
            }
        }
        static void ShowWithdraw()
        {
            Console.Clear();
            Console.WriteLine("[Withdraw]");
            Console.Write("Enter amount: ");
            decimal WithAmount;
            if (Decimal.TryParse(Console.ReadLine(), out WithAmount))
            {
                if (b.CurrentAccount.Balance < WithAmount || WithAmount < 0)
                {
                    Console.WriteLine("Insufficient Funds or Invalid Amount");
                }
                else
                {
                    b.CurrentAccount.Withdraw(WithAmount);
                }
            }
        }
        static void ShowTransfer()
        {
            Console.Clear();
            Console.WriteLine("[Transfer]");
            Console.Write("Enter recipient: ");
            string receiverAcc = Console.ReadLine();
            Console.Write("Enter amount: ");
            decimal TranAmount;
            if (Decimal.TryParse(Console.ReadLine(), out TranAmount))
            {
                if (!b.IsAccountExist(receiverAcc))
                {
                    Console.Clear();
                    Console.WriteLine("Recipient does not exist.");
                }
                else if (TranAmount > b.CurrentAccount.Balance || TranAmount < 0)
                {
                    Console.WriteLine("Invalid amount or Insufficient funds");
                }
                else
                {
                    b.Transfer(receiverAcc, TranAmount);
                }
            }
        }
        static void ShowTransactions()
        {
            Console.Clear();
            Console.WriteLine("[Transactions]");
            Console.WriteLine($"DATE:\t\t\tTRANSACTION:\tFROM:\tAMOUNT:\tBALANCE:");
            foreach (Transaction transaction in b.CurrentAccount.GetTransaction())
            {
                string Target = (transaction.Target.Username != null ? transaction.Target.Username : "");
                Console.WriteLine($"{transaction.Date}\t{transaction.Type}\t\t\t{Target}\t{transaction.Amount}\t{transaction.Balance}");
            }
            Console.ReadKey();
        }
    }
}
