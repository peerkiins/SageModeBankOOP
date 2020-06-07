using System;

namespace SageModeBankOOP
{
    class Bank
    {
        private int _TotalAccountsRegistered { get; set; }
        private string _name = "Bank";
        public Account CurrentAccount { get; set; }
        public string Name
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

        private Account[] Accounts { get; set; }

        public Bank()
        {
            Accounts = new Account[100];
            _TotalAccountsRegistered = 0;
        }

        public void Register(string username, string password)
        {
            Accounts[_TotalAccountsRegistered++] = new Account
            {
                Id = _TotalAccountsRegistered,
                Username = username,
                Password = password,
                Balance = 0
            };
        }

        public bool Login(string username, string password)
        {

            for (int i = 0; i < _TotalAccountsRegistered; i++)
            {
                Account account = Accounts[i];
                if (account.Username == username && account.Password == password)
                {
                    CurrentAccount = account;
                    return true;
                }
            }
            return false;
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
        public void Transfer(string ReceiverAccUsername, decimal TransAmount)
        {
            for (int x = 0; x < _TotalAccountsRegistered; x++)
            {
                Account account = Accounts[x];
                if (account.Username == ReceiverAccUsername)
                {
                    account.Balance += TransAmount;
                    CurrentAccount.Balance -= TransAmount;
                    account.AddTransactions("Received", TransAmount, CurrentAccount);
                    CurrentAccount.AddTransactions("Sent", TransAmount, account);
                    break;
                }
            }
            return;
        }



    }
}