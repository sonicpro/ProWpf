using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    public class EntryViewModel
    {
        private Money oldBalance;
        private Entry entry;

        internal EntryViewModel(Money oldBalance, Entry entry)
        {
            this.oldBalance = oldBalance;
            this.entry = entry;
        }

        public MoneyViewModel Withdrawal
        {
            get
            {
                var amount = Money.Undefined;
                if (entry.EntryType == EntryType.Withdrawal)
                {
                    amount = entry.Amount;
                }

                return new MoneyViewModel(amount);
            }
        }

        public MoneyViewModel Deposit
        {
            get
            {
                var amount = Money.Undefined;
                if (entry.EntryType == EntryType.Deposit)
                {
                    amount = entry.Amount;
                }

                return new MoneyViewModel(amount);
            }
        }

        public MoneyViewModel CurrentBalance
        {
            get
            {
                var balance = entry.ApplyEntry(oldBalance);
                return new MoneyViewModel(balance);
            }
        }
    }
}
