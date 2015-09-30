//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

