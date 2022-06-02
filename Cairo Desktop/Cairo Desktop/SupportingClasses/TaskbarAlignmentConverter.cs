using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CairoDesktop.SupportingClasses
{
    [ValueConversion(typeof(HorizontalAlignment), typeof(int))]
    public class TaskbarAlignmentConverter : IValueConverter
    {
        private object Convert(object value, object parameter, Type targetType)
        {
            Debug.WriteLine($"C {value} {value.GetType().Name} -> {targetType.Name} {(targetType.GenericTypeArguments.Length > 0 ? targetType.GenericTypeArguments[0].Name : null)}");
            if (value is bool)
            {
                return parameter;
            }


            if (value is int v && Enum.IsDefined(typeof(HorizontalAlignment), value))
            {
                if (targetType == typeof(HorizontalAlignment))
                {
                    return (HorizontalAlignment)v;
                }
                if (targetType == typeof(int?))
                {
                    return (int?)v;
                }
                if (targetType == typeof(int))
                {
                    return (int)v;
                }

                if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    if (parameter is string matchStr && int.TryParse(matchStr, out var match))
                    {
                        return match == v;
                    }
                }
            }

            throw new ArgumentException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter, targetType);

            if (value is bool)
            {
                return parameter;
            }

            // Enum has base type int too, so the direction does not matter
            if (!(value is int v)) throw new ArgumentException("Invalid horizontal alignment value");
            Debug.WriteLine($"{value.GetType().Name} -> {targetType.Name} {(targetType.GenericTypeArguments.Length > 0 ? targetType.GenericTypeArguments[0].Name : null)}");

            if (parameter is int match)
            {
                return match == v;
            }

            var cast = (HorizontalAlignment)v;
            switch (cast)
            {
                case HorizontalAlignment.Left:
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Right:
                    return (int)cast;
                case HorizontalAlignment.Stretch:
                default:
                    throw new NotImplementedException("Unhandled alignment");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter, targetType);

            if (value is bool)
            {
                return parameter;
            }

            Debug.WriteLine($"{targetType.Name} <- {value.GetType().Name}");
            if (!(value is int v)) throw new ArgumentException("Invalid horizontal alignment value");

            if (parameter is int match)
            {
                return match == (int)value;
            }

            switch ((int)value)
            {
                case 1:
                    return HorizontalAlignment.Left;
                case 2:
                    return HorizontalAlignment.Center;
                case 3:
                    return HorizontalAlignment.Right;
                default:
                    throw new NotImplementedException("Unhandled alignment value");
            }
        }
    }
}
