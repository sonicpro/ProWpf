using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BooleanToValueConverterExample
{
    internal class MyBooleanToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                Visibility falsyValue;
                try
                {
                    falsyValue = (Visibility)parameter;
                }
                catch
                {
                    falsyValue = Visibility.Hidden;
                }

                try
                {
                    bool sourceBoolean = (bool)value;
                    return sourceBoolean ? Visibility.Visible : falsyValue;
                }
                catch
                {
                    return falsyValue;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
