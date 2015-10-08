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
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Statics;

namespace XamarinCRM.Cells.Customers
{
    public class CustomerOrderListViewGroupHeaderCell : ViewCell
    {
        public CustomerOrderListViewGroupHeaderCell()
        {
            var title = new Label
            {
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            };
            title.SetBinding(Label.TextProperty, "Key");

            var contentView = new ContentView() { Content = title, HeightRequest = Sizes.MediumRowHeight, Padding = new Thickness(10, 0) };

            contentView.SetBinding(StackLayout.BackgroundColorProperty, "Key", converter: new OrderListHeaderViewBackgroudColorConverter());

            View = contentView;
        }
    }
}

