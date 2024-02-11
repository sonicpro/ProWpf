using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SampleApplicationModel;

[assembly: InternalsVisibleTo("SampleApplicationViewModelTests")]
namespace SampleApplicationViewModel
{
    public class AccountViewModel
    {
        private IAccount account;
        private ObservableCollection<AccountViewModel> childAccounts;

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
    }
}
