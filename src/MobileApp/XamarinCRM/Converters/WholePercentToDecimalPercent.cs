
using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinCRM.Converters
{
    public class WholePercentToDecimalPercent : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 100;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 100;
        }
        #endregion

    }
}
