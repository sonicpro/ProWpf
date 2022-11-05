using System;
using System.Globalization;
using System.Windows.Media;

namespace MultiplePropPocoValueConverter
{
    public class DomainObject
    {
        public Color ConvertQuarterAndBalanceToColor(string month, decimal balance)
        {
            if (balance == 0M)
            {
                return Colors.Black;
            }

            if (balance > 0M)
            {
                return Colors.Green;
            }

            var quarter = GetQuarterByMonthName(month);
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

        private int GetQuarterByMonthName(string month)
        {
            DateTime.TryParseExact(month, "MMMM", CultureInfo.CurrentCulture.DateTimeFormat,
                DateTimeStyles.AssumeUniversal, out var monthDateTime);
            return (monthDateTime.Month + 2) / 3;
        }
    }
}