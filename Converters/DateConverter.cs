using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AutoGears.Converters
{
    public class DateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not null && value is DateTime dateTime) return new DateTimeOffset(dateTime);
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset dateTimeOffset) return dateTimeOffset.DateTime;
            return null;
        }
    }
}
