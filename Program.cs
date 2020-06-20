using System;

namespace SageModeBankOOP
{
    class Program
    {
        static string TempBankName = String.Empty;
        static string TempBankAbbv = String.Empty;
        static string TempUsername = String.Empty;
        static string TempPassword = string.Empty;
        static Account CurrentAccount;
        static Bank CurrentBank;
        public Bank[] Banks = new Bank[10];
        private int _bankcounter { get; set; }

        static void Main()
        {
            Program p = new Program();
            bool shouldExit = false;
            while (!shouldExit)
            {
                switch (ShowMenu("Choose Bank", "Register a new bank", "Exit"))
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("[List of Registered Banks]");
                        p.ShowBankList();
                        Console.Write("\n[Select Bank]");
                        Console.Write("\nPlease Enter your bank's Id: ");
                        int TempBankId;
                        int.TryParse(Console.ReadLine(), out TempBankId);
                        CurrentBank = p.EnterChosenBanks(TempBankId);
                        if (CurrentBank != null)
                        {
                            bool ExitBank = false;
                            while (!ExitBank)
                            {
                                switch (ShowMenu("Register New Account", "Login Account", "Change Bank", "Exit"))
                                {
                                    case '1':
                                        Console.Clear();
                                        Console.Write("\n[Account Registration]");
                                        Console.Write("Enter Username: ");
                                        TempUsername = Console.ReadLine().Trim();
                                        if (TempUsername.Length < 4)
                                        {
                                            Console.WriteLine("Username contains an invalid character");
                                        }
                                        else if (CurrentBank.IsAccountExist(TempUsername))
                                        {
                                            Console.WriteLine("Account already existed");
                                        }
                                        else
                                        {
                                            Console.Write("\nEnter your password: ");
                                            TempPassword = Console.ReadLine();
                                            CurrentBank.RegisterAccount(TempUsername, TempPassword);
                                            Console.WriteLine("Success!");
                                        }
                                        continue;
                                    case '2':
                                        Console.Clear();
                                        Console.Write("\n[Login]");
                                        Console.Write("Enter Username: ");
                                        TempUsername = Console.ReadLine().Trim();
                                        Console.Write("\nEnter your password: ");
                                        TempPassword = Console.ReadLine();
                                        CurrentAccount = CurrentBank.LoginAccount(TempUsername, TempPassword);
                                        if (CurrentAccount != null)
                                        {
                                            bool IsLoggedOut = false;
                                            while (!IsLoggedOut)
                                            {
                                                switch (ShowMenu("Deposit", "Withdraw", "Transfer", "Transactions", "Logout", "Exit"))
                                                {
                                                    case '1':
                                                        Console.Clear();
                                                        Console.WriteLine("[Deposit]");
                                                        Console.Write("\nEnter amount to deposit: ");
                                                        decimal DEPAmount;
                                                        decimal.TryParse(Console.ReadLine(), out DEPAmount);
                                                        Console.WriteLine(CurrentAccount.Deposit(DEPAmount));
                                                        continue;
                                                    case '2':
                                                        Console.Clear();
                                                        Console.WriteLine("[Withdraw]");
                                                        Console.Write("\nEnter amount to withdraw: ");
                                                        decimal WDLAmount;
                                                        decimal.TryParse(Console.ReadLine(), out WDLAmount);
                                                        Console.WriteLine(CurrentAccount.Withdraw(WDLAmount));
                                                        continue;
                                                    case '3':
                                                        Console.Clear();
                                                        Console.WriteLine("Fund Transfer]");
                                                        Console.Write("Enter recipient's account ID: ");
                                                        int ReceiverId;
                                                        int.TryParse(Console.ReadLine(), out ReceiverId);
                                                        Account Receiver = CurrentBank.ReceiverAccount(ReceiverId);
                                                        Console.Write("\nEnter amount to transfer: ");
                                                        decimal TXRAmount;
                                                        decimal.TryParse(Console.ReadLine(), out TXRAmount);
                                                        Console.WriteLine(CurrentBank.Transfer(TXRAmount, CurrentAccount, Receiver));
                                                        continue;
                                                    case '4':
                                                        Console.Clear();
                                                        Console.WriteLine("[Transactions]");
                                                        Console.WriteLine("DATE\t\t\tTYPE\t\tSENDER\t\tRECEIVER\t\tAMOUNT\t\tBALANCE");
                                                        foreach (Transaction transaction in CurrentAccount.GetTransactions())
                                                        {
                                                            string Sender = transaction.Sender.Username;
                                                            string TReceiver = transaction.Receiver.Username;
                                                            Console.WriteLine($"{transaction.Date}\t\t{transaction.Type}\t\t{Sender}\t\t{TReceiver}\t\t{transaction.Amount}\t\t{transaction.Balance}");
                                                        }
                                                        continue;
                                                    case '5':
                                                        IsLoggedOut = true;
                                                        continue;
                                                    case '6':
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Login Failed, Username and Password does not match. Try again.");
                                        }
                                        continue;
                                    case '3':
                                        ExitBank = true;
                                        continue;
                                    case '4':
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Entering bank failed, please try again..");
                        }
                        continue;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("\n[Register Bank]");
                        Console.Write("\nPlease Enter your bank's name: ");
                        TempBankName = Console.ReadLine().Trim();
                        Console.Write("Enter desired Abbreviation: ");
                        TempBankAbbv = Console.ReadLine().Trim();
                        if (TempBankName.Length >= 5 && TempBankAbbv.Length <= 1)
                        {
                            Console.WriteLine("Bankname must be more than 5 characters and Bankcode must be atleast 2 characters.");
                        }
                        else if (p.isBankExist(TempBankName, TempBankAbbv))
                        {
                            Console.WriteLine("Bankname or Bankcode is already registered in our system.");
                        }
                        else
                        {
                            p.RegisterBank(TempBankName, TempBankAbbv);
                        }
                        continue;
                    case '3':
                        shouldExit = true;
                        break;
                }
            }
        }
        static char ShowMenu(params string[] items)
        {
            string menuString = "Press ";
            for (int i = 0; i < items.Length; i++)
            {
                string postFix = i == items.Length - 1 ? string.Empty : ",";
                menuString += $"{i + 1} to {items[i]}{postFix}";
            }
            Console.WriteLine($"\n{menuString}, ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            return key.KeyChar;
        }
        public void ShowBankList()
        {
            Console.WriteLine("Bank Id\t\tBank name\t\tAbbv.");
            foreach (Bank bank in Banks)
            {
                for (int x = 0; x < _bankcounter; x++)
                {
                    Console.WriteLine($"{bank.Id}\t\t{bank.Name}\t\t{bank.Abbv}");
                }
            }
        }
        public Bank EnterChosenBanks(int TempBankId)
        {
            foreach (Bank bank in Banks)
            {
                if (bank != null && TempBankId == bank.Id)
                {
                    return bank;
                }
            }
            return null;
        }
        public bool isBankExist(string TempBankName, string TempBankCode)
        {
            foreach (Bank bank in Banks)
            {
                if (bank != null && TempBankName == bank.Name || TempBankCode == bank.Abbv)
                {
                    return true;
                }
            }
            return false;
        }
        public void RegisterBank(string TempBankName, string TempBankCode)
        {
            foreach (Bank bank in Banks)
            {
                if (_bankcounter == Banks.Length - 3)
                {
                    BankArrLengthResize();
                    Banks[_bankcounter++] = new Bank
                    {
                        Id = _bankcounter,
                        Name = TempBankName,
                        Abbv = TempBankCode,
                    };
                }
                else
                {
                    Banks[_bankcounter++] = new Bank
                    {
                        Id = _bankcounter,
                        Name = TempBankName,
                        Abbv = TempBankCode,
                    };
                }
            }
        }
        public void BankArrLengthResize()
        {
            Bank[] TempBanks = new Bank[Banks.Length + 10];
            for (int x = 0; x < _bankcounter; x++)
            {
                TempBanks[x] = Banks[x];
                Banks = TempBanks;
            }
        }
    }
}