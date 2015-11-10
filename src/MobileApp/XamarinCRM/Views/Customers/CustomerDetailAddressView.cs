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
using XamarinCRM.Pages.Customers;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM
{
    public class CustomerDetailAddressView : ModelBoundContentView<CustomerDetailViewModel>
    {
        public CustomerDetailAddressView()
        {
            #region labels
            Label addressTitleLabel = new Label()
            { 
                Text = TextResources.Customers_Detail_Address,
                TextColor = Device.OnPlatform(Palette._003, Palette._007, Palette._006),
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label addressStreetLabel = new Label()
            { 
                TextColor = Palette._006, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStreetLabel.SetBinding(Label.TextProperty, "Account.Street");

            Label addressCityLabel = new Label()
            { 
                TextColor = Palette._006, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressCityLabel.SetBinding(Label.TextProperty, "Account.City");

            Label addressStatePostalLabel = new Label()
            { 
                TextColor = Palette._006, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStatePostalLabel.SetBinding(Label.TextProperty, "Account.StatePostal");
            #endregion

            #region map marker image
            Image mapMarkerImage = new Image()
            { 
                Source = new FileImageSource { File = Device.OnPlatform("map_marker_ios", "map_marker_android", null) }, 
                Aspect = Aspect.AspectFit, 
                HeightRequest = 25
            }; 

            // an expanded view to catch touches, because the image is a bit small
            AbsoluteLayout mapMarkerImageTouchView = new AbsoluteLayout()
            { 
                WidthRequest = Sizes.MediumRowHeight, 
                HeightRequest = Sizes.MediumRowHeight
            };

            mapMarkerImageTouchView.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    Command = new Command(MapMarkerIconTapped) 
                });

            mapMarkerImageTouchView.Children.Add(mapMarkerImage, new Rectangle(.5, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout()
            { 
                Children =
                {
                    addressTitleLabel,
                    addressStreetLabel,
                    addressCityLabel,
                    addressStatePostalLabel

                },
                Padding = new Thickness(20) 
            };
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            absoluteLayout.Children.Add(mapMarkerImageTouchView, new Rectangle(.95, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);
            #endregion

            Content = absoluteLayout;
        }

        async void MapMarkerIconTapped()
        {
            var navPage = new CustomerMapPage(ViewModel)
                {
                    Title = "Location"
                };
            await ViewModel.PushAsync(navPage);
        }
    }
}

