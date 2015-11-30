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
using Xamarin.Forms;
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
                WidthRequest = RowSizes.MediumRowHeightDouble, 
                HeightRequest = RowSizes.MediumRowHeightDouble
            };

            mapMarkerImageTouchView.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    Command = new Command(MapMarkerIconTapped) 
                });

            mapMarkerImageTouchView.Children.Add(mapMarkerImage, new Rectangle(.5, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new StackLayout()
            { 
                Spacing = 0,
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

