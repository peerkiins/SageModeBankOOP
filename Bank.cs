using System.Collections.Generic;

namespace SageModeBankOOP
{
    class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbv { get; set; }
        private int _totalaccountsregistered { get; set; }
        public Account[] Accounts { get; set; }

        public Bank()
        {
            Accounts = new Account[50];
            _totalaccountsregistered = 0;
        }
        public bool IsAccountExist(string TempUsername)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && TempUsername == account.Username)
                    return true;
            }
            return false;
        }
        public void RegisterAccount(string TempUsername, string Temppassword)
        {
            if (_totalaccountsregistered == Accounts.Length - 5)
            {
                AccArrLengthResize();
                Accounts[_totalaccountsregistered++] = new Account
                {
                    Username = TempUsername,
                    Password = Temppassword,
                    Id = _totalaccountsregistered
                };
            }
            else
            {
                Accounts[_totalaccountsregistered++] = new Account
                {
                    Username = TempUsername,
                    Password = Temppassword,
                    Id = _totalaccountsregistered
                };
            }
        }
        public void AccArrLengthResize()
        {
            Account[] TempAccounts = new Account[Accounts.Length + 50];
            for (int x = 0; x < _totalaccountsregistered; x++)
            {
                TempAccounts[x] = Accounts[x];
                Accounts = TempAccounts;
            }
        }
        public Account LoginAccount(string TempUsername, string Temppassword)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == TempUsername && account.Password == Temppassword)
                {
                    return account;
                }
            }
            return null;
        }
        public Account ReceiverAccount(int AccountId)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Id == AccountId)
                {
                    return account;
                }
            }
            return null;
        }
        public string Transfer(decimal Amount, Account Sender, Account Receiver)
        {
            if (Amount <= 0)
            {
                return "Invalid Amount!";
            }
            else if (Amount > Sender.Balance)
            {
                return "Insufficient Funds";
            }
            else
            {
                Sender.Balance -= Amount;
                Sender.Addtransaction("TXR", Amount, Sender, Receiver);
                Receiver.Balance += Amount;
                Receiver.Addtransaction("TXR", Amount, Sender, Receiver);
                return "Success!";
            }
        }
    }
}