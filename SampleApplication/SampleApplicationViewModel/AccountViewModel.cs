using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SampleApplicationModel;

[assembly: InternalsVisibleTo("SampleApplicationViewModelTests")]
namespace SampleApplicationViewModel
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private IAccount account;
        private ObservableCollection<AccountViewModel> childAccounts;
        private BindingList<EntryViewModel> entries;

        public AccountViewModel(IAccount account)
        {
            this.account = account;
        }

        public ObservableCollection<AccountViewModel> ChildAccounts
        {
            get
            {
                ObservableCollection<AccountViewModel> returnValue;
                if (childAccounts != null)
                {
                    returnValue = childAccounts;
                }
                else
                {
                    childAccounts = new ObservableCollection<AccountViewModel>();
                    if (account is CompositeAccount compositeAccount)
                    {
                        childAccounts = new ObservableCollection<AccountViewModel>(compositeAccount.ChildAccounts.Select(ca => new AccountViewModel(ca)));
                    }

                    returnValue = childAccounts;
                }

                return returnValue;
            }
        }

        public BindingList<EntryViewModel> Entries
        {
            get
            {
                if (entries == null)
                {
                    entries = new BindingList<EntryViewModel>();
                    entries.AddingNew += new AddingNewEventHandler(EntriesAddingNew);
                    if (IsLeafAccount)
                    {
                        var runningBalance = Money.Zero;
                        foreach (var entry in (account as LeafAccount).Entries)
                        {
                            var newEntry = new EntryViewModel(this, entry);
                            entries.Add(newEntry);
                        }
                    }
                }

                return entries;
            }
        }

        public MoneyViewModel BalanceAt(EntryViewModel entryViewModel)
        {
            return new MoneyViewModel((account as LeafAccount).BalanceAt(entryViewModel.Entry));
        }

        internal IAccount Account => account;

        private void EntriesAddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new EntryViewModel(this);
        }

        private bool IsLeafAccount => account is LeafAccount;

        internal void EntryChanged()
        {
            foreach (var entryViewModel in entries)
            {
                entryViewModel.BalanceChanged();
            }

            OnPropertyChanged("CurrentBalance");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
