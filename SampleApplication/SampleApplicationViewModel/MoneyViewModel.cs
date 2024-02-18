using System.Runtime.CompilerServices;
using SampleApplicationModel;

[assembly: InternalsVisibleTo("SampleApplicationViewModelTests")]
namespace SampleApplicationViewModel
{
    public class MoneyViewModel
    {
        private Money money;
        
        // We specified [assembly: InternalsVisibleTo("SampleApplicationViewModelTests")] attribute,
        // so we can construct MoneyViewModel in "SampleApplicationViewModelTests" project.
        internal MoneyViewModel(Money money)
        {
            this.money = money;
        }

        public string DisplayValue => money.Amount.ToString("C", money.Currency);

        internal Money Money => money;
    }
}
