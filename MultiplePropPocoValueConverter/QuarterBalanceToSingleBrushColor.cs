using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MultiplePropPocoValueConverter
{
    internal class QuarterBalanceToSingleBrushColor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return null;
            }

            string month = null;
            decimal balance = decimal.MinValue;
            foreach (var v in values)
            {
                if (v is string s)
                {
                    month = s;
                } else if (v is decimal @decimal)
                {
                    balance = @decimal;
                }
            }

            DateTime monthDateTime;
            if (!DateTime.TryParseExact(month, "MMMM", System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal, out monthDateTime))
            {
                return null;
            }

            int quarter = (monthDateTime.Month + 2) / 3;
            return GetQuarterBalanceColor(quarter, balance);
        }

        /// <summary>
        /// The close FY end, the more "intimidating" the color becomes for negative balances.
        /// </summary>
        /// <param name="quarter">Q number</param>
        /// <param name="balance">The balance that corresponds to a date withing the quarter</param>
        /// <returns></returns>
        private static object GetQuarterBalanceColor(int quarter, decimal balance)
        {
            if (balance > 0M)
            {
                return Colors.Green;
            }

            if (balance == 0M)
            {
                return Colors.Black;
            }

            switch (quarter)
            {
                case 1:
                    return Colors.Yellow;
                case 2:
                    return Colors.DarkOrange;
                case 3:
                    return Colors.OrangeRed;
                case 4:
                    return Colors.Red;
                default:
                    return Colors.Magenta;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
