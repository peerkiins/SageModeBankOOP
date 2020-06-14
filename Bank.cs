using System;

namespace SageModeBankOOP
{
    class Bank
    {
        private int _totalaccountstegistered { get; set; }
        private int _newaccounttreshold { get; set; }
        public int BankCode { get; set; }
        private string _name = "Bank";

        public string BankName
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value + " Bank";
            }
        }
        public string BankAbbv { get; set; }

        private Account[] Accounts { get; set; }

        public Bank()
        {
            Accounts = new Account[100];
            _totalaccountstegistered = 0;
        }

        public void Register(string username, string password)
        {
            _newaccounttreshold = Accounts.Length - 15;
            if (_totalaccountstegistered == _newaccounttreshold)
            {
                AccountLengthResize();
                Accounts[_totalaccountstegistered++] = new Account
                {
                    UserId = _totalaccountstegistered,
                    Username = username,
                    Password = password,
                    Balance = 0
                };
            }
            else
            {
                Accounts[_totalaccountstegistered++] = new Account
                {
                    UserId = _totalaccountstegistered,
                    Username = username,
                    Password = password,
                    Balance = 0
                };
            }
        }

        public Account Login(string username, string password)
        {

            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == username && account.Password == password)
                {
                    return account;
                }
            }
            return null;
        }

        public bool IsAccountExist(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == username)
                    return true;
            }
            return false;
        }
        public void Transfer(Account Sender, int ReceiverAccUserId, decimal TransAmount)
        {
            Account Receiver = Accounts[ReceiverAccUserId];
            if (Receiver != null)
            {
                if (Sender.Balance < TransAmount)
                {
                    Console.Clear();
                    Console.WriteLine("Insufficient Balance");
                }
                else if (TransAmount < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Amount");
                }
                else
                {
                    Receiver.Balance += TransAmount;
                    Sender.Balance -= TransAmount;
                    Receiver.AddTransactions("Received", TransAmount, Sender, Receiver);
                    Sender.AddTransactions("Sent", TransAmount, Receiver, Sender);
                }
            }
            return;
        }
        public void AccountLengthResize()
        {
            Account[] TempAccnts = new Account[Accounts.Length + 100];
            for (int y = 0; y < _totalaccountstegistered; y++)
            {
                TempAccnts[y] = Accounts[y];
                Accounts = TempAccnts;
            }
        }
    }
}