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

        #region Object overrides.
        /// <summary>
        /// This overload is for any place in the App that requires displaying MoneyViewModel without a prefix.
        /// For the cases with prefix we have a DataTemplate in App.xaml.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayValue;
        }

        #endregion
    }
}
