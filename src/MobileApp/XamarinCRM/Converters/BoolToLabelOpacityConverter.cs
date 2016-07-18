
using System;
using Xamarin.Forms;
using System.Globalization;

namespace XamarinCRM.Converters
{
    public class BoolToLabelOpacityConverter : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return 0.5d;
            }
            else
            {
                return 1.0d;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

