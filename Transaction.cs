using System;

namespace SageModeBankOOP
{
    class Transaction
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public Account Target { get; set; }
    }
}