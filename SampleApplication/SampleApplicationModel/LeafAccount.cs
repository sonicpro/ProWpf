using System.Collections.Generic;

namespace SampleApplicationModel
{
    public class LeafAccount : IAccount
    {
        private readonly List<Entry> entries = new List<Entry>();
        private readonly Money openingBalance;

        public LeafAccount(string name) : this(name, Money.Zero)
        {
        }

        public LeafAccount(string name, Money depositOnOpening)
        {
            Name = name;
            openingBalance = depositOnOpening;
        }

        public string Name { get; }

        public Money Balance
        {
            get
            {
                var balance = openingBalance;
                foreach (var e in entries)
                {
                    balance = e.ApplyEntry(balance);
                }

                return balance;
            }
        }

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public List<Entry> Entries => entries;

        public IAccount Parent { get; set; }

        public Money BalanceAt(Entry entry)
        {
            Money balanceAt = openingBalance;
            foreach (var currentEntry in entries)
            {
                balanceAt = currentEntry.ApplyEntry(balanceAt);
                if (currentEntry == entry)
                {
                    break;
                }
            }

            return balanceAt;
        }
    }
}
