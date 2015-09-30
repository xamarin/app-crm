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
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Converters
{
    public class OrderListHeaderViewBackgroudColorConverter : IValueConverter
	{
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((string)value == TextResources.Customers_OrderOpen)
                return Palette._003;

            if ((string)value == TextResources.Customers_OrderClosed)
                return Palette._017;

            return Palette._008;
            
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
	}
}

