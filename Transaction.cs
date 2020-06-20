using System;

namespace SageModeBankOOP
{
    class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public Account Sender { get; set; }
        public Account Receiver { get; set; }
    }
}