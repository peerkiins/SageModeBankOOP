namespace SageModeBankOOP
{
    class Account
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        private int _transcount { get; set; }
        private int _transfercounttreshold { get; set; }
        private Transaction[] Transactions { get; set; }
        public Account()
        {
            Transactions = new Transaction[250];
            _transcount = 0;
        }
        public void AddTransactions(string transaction, decimal amount, Account sender, Account receiver)
        {
            _transfercounttreshold = Transactions.Length - 25;
            if (_transcount == _transfercounttreshold)
            {
                TransactionLengthResize();
                Transactions[_transcount++] = new Transaction
                {
                    Date = System.DateTime.Now,
                    Type = transaction,
                    Amount = amount,
                    Balance = Balance,
                    Sender = sender,
                    Receiver = receiver
                };
            }
            else
            {
                Transactions[_transcount++] = new Transaction
                {
                    Date = System.DateTime.Now,
                    Type = transaction,
                    Amount = amount,
                    Balance = Balance,
                    Sender = sender,
                    Receiver = receiver
                };
            }
        }

        public void Withdraw(decimal WithAmount)
        {
            Balance -= WithAmount;
            AddTransactions("Withdraw", WithAmount, this, null);
        }

        public void Deposit(decimal DepAmount)
        {
            Balance += DepAmount;
            AddTransactions("Deposit", DepAmount, null, this);
        }

        public void TransactionLengthResize()
        {
            Transaction[] TempTrans = new Transaction[Transactions.Length + 100];
            for (int y = 0; y < _transcount; y++)
            {
                TempTrans[y] = Transactions[y];
                Transactions = TempTrans;
            }
        }
        public Transaction[] GetTransaction()
        {
            Transaction[] Result = new Transaction[_transcount];
            for (int x = 0; x < _transcount; x++)
            {
                Result[x] = Transactions[x];
            }
            return Result;
        }
    }
}