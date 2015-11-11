// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Diagnostics;
using System.Globalization;
using XamarinCRM.Properties.Attributes;
using Xamarin.Forms;

namespace XamarinCRM.Converters
{
    public class ConvertableConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(value.ToString(), new []{ "ConvertableConverter.Convert" });
            if ((parameter == null || !(parameter is CurrencyAttribute)))
                return System.Convert.ChangeType(value, targetType);

            return string.Format(culture.NumberFormat, "{0:C}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(value.ToString(), new []{ "ConvertableConverter.ConvertBack" });


            // Handle money in a localization-aware manner.
            if (targetType == typeof(Decimal) && value is string && ((string)value).StartsWith(NumberFormatInfo.CurrentInfo.CurrencySymbol, StringComparison.CurrentCultureIgnoreCase))
            {
                var val = Decimal.Parse((string)value, NumberStyles.Currency);
                return val;
            }
            var result = Convert(value, targetType, parameter, culture);
            return result;
        }

        #endregion
    }
}

