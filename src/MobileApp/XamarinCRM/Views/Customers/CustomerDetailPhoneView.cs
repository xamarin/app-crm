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
using Xamarin.Forms;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using Lotz.Xam.Messaging;

namespace XamarinCRM
{
    public class CustomerDetailPhoneView : ModelBoundContentView<CustomerDetailViewModel>
    {
        readonly Page _Page;

        public CustomerDetailPhoneView(Page page)
        {
            _Page = page;

            #region labels
            Label phoneTitleLabel = new Label()
            { 
                Text = TextResources.Phone,
                TextColor = Device.OnPlatform(Palette._003, Palette._007, Palette._006),
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label phoneLabel = new Label()
            { 
                TextColor = Palette._006, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            phoneLabel.SetBinding(Label.TextProperty, "Account.Phone");
            #endregion

            #region phone image
            Image phoneImage = new Image()
            { 
                Source = new FileImageSource { File = Device.OnPlatform("phone_ios", "phone_android", null) }, 
                Aspect = Aspect.AspectFit, 
                HeightRequest = 25 
            }; 

            // an expanded view to catch touches, because the image is a bit small
            AbsoluteLayout phoneImageTouchView = new AbsoluteLayout()
            { 
                WidthRequest = RowSizes.MediumRowHeightDouble, 
                HeightRequest = RowSizes.MediumRowHeightDouble
            };

            phoneImageTouchView.Children.Add(phoneImage, new Rectangle(.5, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);

            phoneImageTouchView.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    Command = new Command(() => OnPhoneTapped(phoneLabel, null)) 
                });
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new StackLayout()
            {
                Spacing = 0,
                Children =
                {
                    phoneTitleLabel,
                    phoneLabel
                },
                Padding = new Thickness(20) 
            };
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            absoluteLayout.Children.Add(phoneImageTouchView, new Rectangle(.95, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);
            #endregion

            Content = absoluteLayout;
        }

        async void OnPhoneTapped(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            string phoneNumber = ((Label)sender).Text;

            if (String.IsNullOrWhiteSpace(phoneNumber))
                return;        

            if (await _Page.DisplayAlert(
                    title: TextResources.Customers_Detail_CallDialog_Title,
                    message: TextResources.Customers_Detail_CallDialog_Message + phoneNumber + "?",
                    accept: TextResources.Customers_Detail_CallDialog_Accept,
                    cancel: TextResources.Customers_Detail_CallDialog_Cancel))
            {
                var phoneCallTask = MessagingPlugin.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                    phoneCallTask.MakePhoneCall(phoneNumber.Replace("-", ""));
            }
        }
    }
}

