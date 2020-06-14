namespace SageModeBankOOP
{
    class BankNet
    {
        public int _totalregisteredbank { get; set; }
        public int _bankregistertreshold { get; set; }
        public Bank[] Banks { get; set; }

        public bool IsBankRegistered(string Bankname)
        {
            foreach (Bank Bank in Banks)
            {
                if (Bank != null && Bank.BankName == Bankname)
                {
                    return true;
                }
            }
            return false;
        }
        public void RegisterBank(string Bankname, string abbv)
        {
            _bankregistertreshold = Banks.Length - 3;
            if (_totalregisteredbank == _bankregistertreshold)
            {
                Banks[_totalregisteredbank++] = new Bank
                {
                    BankCode = _totalregisteredbank,
                    BankName = Bankname,
                    BankAbbv = abbv
                };
            }
            else
            {
                Banks[_totalregisteredbank++] = new Bank
                {
                    BankCode = _totalregisteredbank,
                    BankName = Bankname,
                    BankAbbv = abbv
                };
            }
        }
        public Bank EnterBank(string Bankname)
        {
            foreach (Bank Bank in Banks)
            {
                if (Bank != null && Bank.BankName == Bankname)
                {
                    return Bank;
                }
            }
            return null;
        }
        public BankNet()
        {
            Banks = new Bank[10];
            _totalregisteredbank = 0;
        }
    }
}