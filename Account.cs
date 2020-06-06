namespace SageModeBankOOP
{
    class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        private int _TransCount { get; set; }
        public Transaction[] Transactions { get; set; }

        public Account()
        {
            Transactions = new Transaction[1000];
            _TransCount = 0;
        }
        public void AddTransaction(string transaction, decimal amount)
        {
            Transactions[_TransCount] = new Transaction
            {
                Date = System.DateTime.Now,
                Type = transaction,
                Amount = amount,
                Target = this,
            };
            _TransCount++;
        }

        public void Withdraw(decimal WithAmount)
        {
            Balance -= WithAmount;
            AddTransaction("Withdraw", WithAmount);
        }

        public void Deposit(decimal DepAmount)
        {
            Balance += DepAmount;
            AddTransaction("Deposit", DepAmount);
        }

        public void Send(decimal SendAmount)//outbound funds
        {
            Balance -= SendAmount;
            AddTransaction("Send", SendAmount);
        }
    }
}