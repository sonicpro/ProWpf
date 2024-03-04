using System.ComponentModel;
using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    public class EntryViewModel : INotifyPropertyChanged, IEditableObject
    {
        private AccountViewModel accountViewModel;
        private Entry entry;
        // Needed for IEditableObject implementation.
        private Entry backupEntry;
        private bool isInTransaction;

        public EntryViewModel()
        {
            entry = new Entry(EntryType.Withdrawal, Money.Zero, "");
        }
        internal EntryViewModel(AccountViewModel accountViewModel)
        {
            this.accountViewModel = accountViewModel;
            this.entry = new Entry(EntryType.Withdrawal, Money.Zero, "");
        }

        internal EntryViewModel(AccountViewModel accountViewModel, Entry entry)
        {
            this.accountViewModel = accountViewModel;
            this.entry = entry;
        }

        /// <summary>
        /// Not used in this sample. See explanation of its intended use
        /// on the page 240 of Gary McLean Hall's book.
        /// </summary>
        public AccountViewModel AccountViewModel
        {
            set
            {
                if (value != null)
                {
                    accountViewModel = value;
                }
            }
        }

        public string Description {
            get
            {
                return entry.Description;
            }

            set
            {
                if (entry.Description != value)
                {
                    entry.Description = value;
                    OnPropertyChanged("Description");
                }
            }
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
            set
            {
                if (entry.EntryType == EntryType.Withdrawal)
                {
                    if (entry.Amount != value.Money)
                    {
                        entry.Amount = value.Money;
                    }
                }
                else
                {
                    entry = new Entry(EntryType.Withdrawal, value.Money, "Withdrawal");
                    OnPropertyChanged("Deposit");
                }

                OnPropertyChanged("Withdrawal");
                OnPropertyChanged("CurrentBalance");
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
            set
            {
                if (entry.EntryType == EntryType.Deposit)
                {
                    if (entry.Amount != value.Money)
                    {
                        entry.Amount = value.Money;
                    }
                }
                else
                {
                    entry = new Entry(EntryType.Deposit, value.Money, "Deposit");
                    OnPropertyChanged("Withdrawal");
                }

                OnPropertyChanged("Deposit");
                OnPropertyChanged("CurrentBalance");
            }
        }

        public MoneyViewModel CurrentBalance => accountViewModel.BalanceAt(this);

        internal Entry Entry => entry;

        private void Save()
        {
            (accountViewModel.Account as LeafAccount).AddEntry(entry);
            accountViewModel.EntryChanged();
            OnPropertyChanged("CurrentBalance");
        }

        internal void BalanceChanged()
        {
            OnPropertyChanged("CurrentBalance");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void IEditableObject.BeginEdit()
        {
            if (!isInTransaction)
            {
                backupEntry = new Entry(entry.EntryType, entry.Amount, entry.Description);
                isInTransaction = true;
            }
        }

        void IEditableObject.CancelEdit()
        {
            if (isInTransaction)
            {
                entry = backupEntry;
                isInTransaction = false;
            }
        }

        void IEditableObject.EndEdit()
        {
            if (isInTransaction)
            {
                Save();
                isInTransaction = false;
            }
        }
    }
}
