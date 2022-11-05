namespace MultiplePropPocoValueConverter
{
    public class DomainObject
    {
        public DomainObject(string month, decimal balance)
        {
            Month = month;
            Balance = balance;
        }

        public string Month { get; }

        public decimal Balance { get; }
    }
}