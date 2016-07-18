
using System;
using Xamarin.Forms;

namespace XamarinCRM.Converters
{
    public class LoadingLabelHeightConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Device.GetNamedSize(NamedSize.Small, typeof(Label));
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

