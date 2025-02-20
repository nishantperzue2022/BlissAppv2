using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace BlissApp.Converters
{
    public class IntToMonthNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(!(value is int))
            {
                return "";
            }

            int Month = (int)value;

            string[] Months = { "","Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };

            return Months[Month];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
