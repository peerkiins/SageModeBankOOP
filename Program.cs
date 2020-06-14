using System;

namespace SageModeBankOOP
{
    class Program
    {
        static string tempUsername = string.Empty;
        static string tempPassword = string.Empty;
        static string tempbankname = string.Empty;
        static string tempbankabbv = string.Empty;
        public Bank[] Banks { get; set; }
        private int _totalbanksregistered { get; set; }
        // static Bank b = new Bank();
        static Account CurrentAccount;
        static BankNet a = new BankNet();
        static Bank CurrentBank;
        static void Main(string[] args)
        {
            bool ShouldExit = false;
            while (!ShouldExit)
            {
                switch (ShowMenu("Register New Bank", "Choose Existing Bank", "Exit"))
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Please enter bank's name: ");
                        tempbankname = Console.ReadLine().Trim();
                        if (tempbankname.Length < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("The bank name you entered is invalid.");
                        }
                        else if (a.IsBankRegistered(tempbankname))
                        {
                            Console.Clear();
                            Console.WriteLine("The bank you entered is already registered.");
                        }
                        else
                            Console.Clear();
                        Console.WriteLine("Enter bank Abbreviation: ");
                        tempbankabbv = Console.ReadLine();
                        a.RegisterBank(tempbankname, tempbankabbv);
                        Console.WriteLine("Success!");
                        Console.ReadKey();
                        continue;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("[Choose Your Bank]");
                        Console.Write("Please enter your bank's name: ");
                        tempbankname = Console.ReadLine().Trim();
                        CurrentBank = a.EnterBank(tempbankname);
                        bool BankOut = false;
                        if (CurrentBank != null)
                        {
                            while (!BankOut)
                            {
                                // b.BankName = "Peerkiins";***
                                bool ShouldLogout = false;
                                while (!ShouldLogout)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Welcome to {CurrentBank.BankName}");

                                    switch (ShowMenu("Register", "Login", "Change Bank", "Exit"))
                                    {
                                        case '1':
                                            ShowRegister();
                                            continue;
                                        case '2':
                                            ShowLogin();
                                            continue;
                                        case '3':
                                            BankOut = true;
                                            continue;
                                        case '4':
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("The bank you entered is not registered in our system, please try again.");
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        ShouldExit = true;
                        return;
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
            tempUsername = Console.ReadLine().Trim();
            if (tempUsername.Length < 4 && tempUsername.Length! > 8)
            {
                Console.WriteLine("Username must be atleast 4 characters and not more than 8 Characters.");
                Console.ReadKey();
            }
            else if (CurrentBank.IsAccountExist(tempUsername))
            {
                Console.WriteLine("Account already existed.");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Please enter your password: ");
                tempPassword = Console.ReadLine();
                CurrentBank.Register(tempUsername, tempPassword);
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
            CurrentAccount = CurrentBank.Login(tempUsername, tempPassword);
            bool ShouldLogout = false;
            if (CurrentAccount != null)
            {
                Console.WriteLine("Login Successful.");
                while (!ShouldLogout)
                {
                    Console.Clear();
                    Console.WriteLine($"Hi, {CurrentAccount.Username}");
                    Console.WriteLine($"your current balance is {CurrentAccount.Balance}");
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
                CurrentAccount.Deposit(DepAmount);
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
                if (CurrentAccount.Balance < WithAmount || WithAmount < 0)
                {
                    Console.WriteLine("Insufficient Funds or Invalid Amount");
                }
                else
                {
                    CurrentAccount.Withdraw(WithAmount);
                }
            }
        }
        static void ShowTransfer()
        {
            Console.Clear();
            Console.WriteLine("[Transfer]");
            Console.Write("Enter recipient Account ID: ");
            int receiverAccId;
            if (int.TryParse(Console.ReadLine(), out receiverAccId))
            {
                Console.Write("Enter amount: ");
                decimal TranAmount;
                if (decimal.TryParse(Console.ReadLine(), out TranAmount))
                {
                    CurrentBank.Transfer(CurrentAccount, receiverAccId, TranAmount);
                }
            }
        }
        static void ShowTransactions()
        {
            Console.Clear();
            Console.WriteLine("[Transactions]");
            Console.WriteLine($"DATE:\t\t\tTRANSACTION:\tFROM:\tTO:\tAMOUNT:\tBALANCE:");
            foreach (Transaction transaction in CurrentAccount.GetTransaction())
            {
                string Sender = (transaction.Sender.Username == null ? transaction.Sender.Username : "");//check
                string Receiver = (transaction.Receiver.Username == null ? transaction.Receiver.Username : "");
                Console.WriteLine($"{transaction.Date}\t{transaction.Type}\t\t\t{Sender}\t{Receiver}\t{transaction.Amount}\t{transaction.Balance}");
            }
            Console.ReadKey();
        }
        public Program()
        {
            Banks = new Bank[10];
            _totalbanksregistered = 0;
        }
    }
}
