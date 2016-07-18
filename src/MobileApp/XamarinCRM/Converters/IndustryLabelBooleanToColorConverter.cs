
using System;
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Converters
{
    public class IndustryLabelBooleanToColorConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Palette._006;
            }
            return Palette._008;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

