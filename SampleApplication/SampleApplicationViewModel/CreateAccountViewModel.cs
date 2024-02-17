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

        public ICommand CreateAccountCommand()
        {
            if (createAccountCommand == null)
            {
                createAccountCommand = new RelayCommand(_ => CreateAccount(), _ => CanCreateAccount());
            }

            return createAccountCommand;
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
