namespace SageModeBankOOP
{
    class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        private int _TransCount { get; set; }
        private Transaction[] Transactions { get; set; }

        public Account()
        {
            Transactions = new Transaction[1000];
            _TransCount = 0;
        }
        public void AddTransactions(string transaction, decimal amount, Account target)
        {
            Transactions[_TransCount++] = new Transaction
            {
                Date = System.DateTime.Now,
                Type = transaction,
                Amount = amount,
                Balance = Balance,
                Target = target
            };
        }

        public void Withdraw(decimal WithAmount)
        {
            Balance -= WithAmount;
            AddTransactions("Withdraw", WithAmount, this);
        }

        public void Deposit(decimal DepAmount)
        {
            Balance += DepAmount;
            AddTransactions("Deposit", DepAmount, this);
        }

        public Transaction[] GetTransaction()
        {
            Transaction[] Result = new Transaction[_TransCount];
            for (int x = 0; x < _TransCount; x++)
            {
                Result[x] = Transactions[x];
            }
            return Result;
        }
    }
}