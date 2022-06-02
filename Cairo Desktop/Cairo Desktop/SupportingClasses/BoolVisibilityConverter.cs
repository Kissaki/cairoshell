using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CairoDesktop.SupportingClasses
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility)) throw new ArgumentException();
            if (!(value is bool v)) throw new ArgumentException();

            return v ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool)) throw new ArgumentException();
            if (!(value is Visibility v)) throw new ArgumentException();

            return v == Visibility.Visible;
        }
    }
}
