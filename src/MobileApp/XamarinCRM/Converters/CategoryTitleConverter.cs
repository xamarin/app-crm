using System;
using Xamarin.Forms;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class CategoryTitleConverter : IValueConverter
    {
        readonly string _Title;

        public CategoryTitleConverter(string title)
        {
            _Title = title;
        }

        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((CatalogCategory)value == null)
            {
                return _Title;
            }
                
            return ((CatalogCategory)value).Name;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

