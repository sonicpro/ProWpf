namespace SampleApplicationModel
{
    public class Entry
    {
        public Entry(EntryType type, Money amount, string description)
        {
            EntryType = type;
            Amount = amount;
            Description = description;
        }

        public EntryType EntryType { get; }

        public Money Amount { get; set; }

        public string Description { get; }

        public Money ApplyEntry(Money previousBalance)
        {
            switch (EntryType)
            {
                case EntryType.Deposit:
                    return previousBalance + Amount;
                case EntryType.Withdrawal:
                    return previousBalance - Amount;
                default:
                    return previousBalance;
            }
        }
    }
}
