using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IPerson person;
        private ObservableCollection<AccountViewModel> accounts;
        private readonly ObservableCollection<AccountViewModel> currentlyViewedAccounts =
            new ObservableCollection<AccountViewModel>();
        private AccountViewModel accountInFocus;
        private RelayCommand openAccountDetailsCommand;
        private RelayCommand closeAccountDetailsCommand;

        public MainWindowViewModel(IPerson person)
        {
            this.person = person;
        }

        public MainWindowViewModel()
        {
            person = new Person();
        }

        /// <summary>
        /// Returns the view model for CreateAccount dialog.
        /// We decided to hide CreateAccountViewModel constructor from the view.
        /// </summary>
        /// <returns>The instance of a view model for CreateAccount dialog.</returns>
        public CreateAccountViewModel CreateAccountViewModel()
        {
            return new CreateAccountViewModel(this);
        }

        public MoneyViewModel NetWorth => new MoneyViewModel(person.NetWorth);

        public ObservableCollection<AccountViewModel> Accounts
        {
            get
            {
                if (accounts == null)
                {
                    accounts = new ObservableCollection<AccountViewModel>(person.Accounts.Select(a => new AccountViewModel(a)));
                }

                return accounts;
            }
        }

        public ObservableCollection<AccountViewModel> CurrentlyViewedAccounts => currentlyViewedAccounts;

        public AccountViewModel AccountInFocus
        {
            get => accountInFocus;
            set
            {
                if (accountInFocus != value)
                {
                    accountInFocus = value;
                    OnPropertyChanged("AccountInFocus");
                }
            }
        }

        public ICommand OpenAccountDetailsCommand
        {
            get
            {
                if (openAccountDetailsCommand == null)
                {
                    openAccountDetailsCommand = new RelayCommand(account => OpenAccountDetails(account as AccountViewModel));
                }

                return openAccountDetailsCommand;
            }
        }

        public ICommand CloseAccountDetailsCommand
        {
            get
            {
                if (closeAccountDetailsCommand == null)
                {
                    closeAccountDetailsCommand = new RelayCommand(account => CloseAccountDetails(account as AccountViewModel));
                }

                return closeAccountDetailsCommand;
            }
        }

        public void AddAccount(IAccount account)
        {
            person.AddAccount(account);
            var accountViewModel = new AccountViewModel(account);
            accountViewModel.PropertyChanged += AccountViewModelPropertyChanged;
            accounts.Add(accountViewModel);
        }

        internal void AccountViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("CurrentBalance", System.StringComparison.OrdinalIgnoreCase))
            {
                OnPropertyChanged("NetWorth");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAccountPropertyChanged(string propertyName)
        {
            if (propertyName.Equals("CurrentBalance", System.StringComparison.OrdinalIgnoreCase))
            {
                OnPropertyChanged("NetWorth");
            }
        }

        private void OpenAccountDetails(AccountViewModel account)
        {
            currentlyViewedAccounts.Add(account);
            AccountInFocus = account;
        }

        private void CloseAccountDetails(AccountViewModel account)
        {
            currentlyViewedAccounts.Remove(account);
            AccountInFocus = currentlyViewedAccounts.FirstOrDefault();
        }
    }
}
