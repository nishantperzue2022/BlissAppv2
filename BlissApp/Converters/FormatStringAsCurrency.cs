using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CarPooling.Mobile.Converters
{
    public class FormatStringAsCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            int Number = 0;

            int.TryParse(value.ToString(), out Number);

            return Number.ToString("N0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
