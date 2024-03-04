using System;
using System.Globalization;
using System.Windows.Data;
using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    /// <summary>
    /// Converts string money value into MoneyViewModel.
    /// </summary>
    public class MoneyViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MoneyViewModel returnValue = null;
            if (value is string numericString)
            {
                if (decimal.TryParse(numericString, out var numericValue))
                {
                    var money = new Money(numericValue);
                    returnValue = new MoneyViewModel(money);
                }
            }

            return returnValue;
        }
    }
}
