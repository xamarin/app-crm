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
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Custom;

namespace XamarinCRM.Views.Customers
{
    public class CustomerDetailContactView : ModelBoundContentView<CustomerDetailViewModel>
    {
        public CustomerDetailContactView()
        {
            #region labels
            Label contactTitleLabel = new Label()
            { 
                Text = TextResources.Contact,
                TextColor = Device.OnPlatform(Palette._003, Palette._007, Palette._006),
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				LineBreakMode = LineBreakMode.TailTruncation
            };

            Label contactLabel = new Label()
            { 
                TextColor = Palette._006, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            contactLabel.SetBinding(Label.TextProperty, "Account.DisplayContact");
            #endregion

            #region compose view hierarchy
            Content = new ContentViewWithBottomBorder()
            { 
                Content = new UnspacedStackLayout()
                { 
                    Children =
                    {
                        contactTitleLabel,
                        contactLabel
                    },
                    Padding = new Thickness(20) 
                } 
            };
            #endregion
        }
    }
}

