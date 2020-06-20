using System;

namespace SageModeBankOOP
{
    class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public decimal Balance { get; set; }
        private Transaction[] Transactions { get; set; }
        private int _TotalTransactionCount { get; set; }
        public Account()
        {
            Transactions = new Transaction[100];
            _TotalTransactionCount = 0;
        }
        public string Deposit(decimal Amount)
        {
            if (Amount <= 0)
            {
                return "Invalid Amount";
            }
            else
            {
                Balance += Amount;
                Addtransaction("DEP", Amount, null, this);
                return "Success!";
            }
        }
        public string Withdraw(decimal Amount)
        {
            if (Amount <= 0)
            {
                return "Invalid Amount";
            }
            else if (Amount > this.Balance)
            {
                return "Insufficient Funds!";
            }
            else
            {
                Balance -= Amount;
                Addtransaction("WDL", Amount, this, null);
                return "Success!";
            }
        }
        public void Addtransaction(string TType, decimal Amount, Account Sender, Account Receiver)
        {
            if (_TotalTransactionCount == Transactions.Length - 10)
            {
                TransArrResize();
                Transactions[_TotalTransactionCount] = new Transaction
                {
                    Date = DateTime.Now,
                    Type = TType,
                    Amount = Amount,
                    Sender = Sender,
                    Receiver = Receiver
                };
            }
            else
            {
                Transactions[_TotalTransactionCount] = new Transaction
                {
                    Date = DateTime.Now,
                    Type = TType,
                    Amount = Amount,
                    Sender = Sender,
                    Receiver = Receiver
                };
            }
        }
        public void TransArrResize()
        {
            Transaction[] TempTransactions = new Transaction[Transactions.Length + 50];
            for (int x = 0; x < _TotalTransactionCount; x++)
            {
                TempTransactions[x] = Transactions[x];
                Transactions = TempTransactions;
            }
        }
        public Transaction[] GetTransactions()
        {
            Transaction[] Copy = new Transaction[_TotalTransactionCount];
            for (int x = 0; x < _TotalTransactionCount; x++)
            {
                Copy[x] = Transactions[x];

            }
            return Copy;
        }
    }
}