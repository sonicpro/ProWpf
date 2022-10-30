namespace MultiplePropPocoValueConverter
{
    internal class DomainObject
    {
        public DomainObject()
        {
            Month = "April";
            Balance = -1.0M;
        }

        public string Month { get; }

        public decimal Balance { get; }
    }
}