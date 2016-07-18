
using System;
using Xamarin.Forms;
using System.Globalization;

namespace XamarinCRM.Converters
{
    public class CompanyLabelBooleanToColorConverter : IValueConverter
    {
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Color.Black;
            }
            return Color.White;

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

