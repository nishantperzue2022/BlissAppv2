using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace BlissApp.Converters
{
    public class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int)
            {
                int Id = (int)value;

                if(Id % 5 == 0)
                {
                    return "#33337C";
                }
                else if (Id % 4 == 0)
                {
                    return "#EAAD46";
                }
                else if (Id % 3 == 0)
                {
                    return "OrangeRed";
                }
                else if (Id % 2 == 0)
                {
                    return "#3DB2FF";
                }
                else if (Id % 1 == 0)
                {
                    return "#0EAF1F";
                }
                else
                {
                    return "#1D2027";
                }
            }
            else
            {
                return "#1D2027";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
