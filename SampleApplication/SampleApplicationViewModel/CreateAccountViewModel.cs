using System.Windows.Input;
using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    public class CreateAccountViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private RelayCommand createAccountCommand;

        internal CreateAccountViewModel(MainWindowViewModel mainViewModel)
        {
            mainWindowViewModel = mainViewModel;
        }

        public string Name { get; set; }

        public string OpeningBalance { get; set; }

        /// <summary>
        /// Remember!
        /// The properties that are bound by means of DynamicResource are shown as unreferenced by CodeLens.
        /// It is popup window, so the data context is DynamicResource to DataObjectProvider in App.xaml.
        /// Do not forget to define those properties as readonly properties. If you made them methods, the binding wont work!
        /// </summary>
        /// <returns>RelayCommand</returns>
        public ICommand CreateAccountCommand
        {
            get
            {
                if (createAccountCommand == null)
                {
                    createAccountCommand = new RelayCommand(_ => CreateAccount(), _ => CanCreateAccount());
                }

                return createAccountCommand;
            }
        }

        public bool CanCreateAccount()
        {
            var hasValidName = !string.IsNullOrWhiteSpace(Name);
            var hasValidOpeningBalance = string.IsNullOrWhiteSpace(OpeningBalance) ||
                                         decimal.TryParse(OpeningBalance, out decimal openingBalance);
            return hasValidName && hasValidOpeningBalance;
        }

        public void CreateAccount()
        {
            LeafAccount account;
            if (string.IsNullOrWhiteSpace(OpeningBalance))
            {
                account = new LeafAccount(Name);
            }
            else
            {
                var openingBalance = decimal.Parse(OpeningBalance);
                account = new LeafAccount(Name, openingBalance);
            }

            mainWindowViewModel.AddAccount(account);
        }
    }
}
