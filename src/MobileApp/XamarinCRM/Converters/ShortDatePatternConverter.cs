
using System;
using Xamarin.Forms;

namespace XamarinCRM.Converters
{
    public class ShortDatePatternConverter : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) // handle nullable DateTime
                return string.Empty;
            return ((DateTime)value).ToLocalTime().ToString("d");
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
        
    }
}

