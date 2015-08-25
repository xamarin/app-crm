using System;
using Xamarin.Forms;
using XamarinCRM.Interfaces;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Custom;

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
                TextColor = Device.OnPlatform(Palette._005, Palette._007, Palette._006),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
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
                WidthRequest = Sizes.MediumRowHeight, 
                HeightRequest = Sizes.MediumRowHeight
            };

            phoneImageTouchView.Children.Add(phoneImage, new Rectangle(.5, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);

            phoneImageTouchView.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    Command = new Command(() => OnPhoneTapped(phoneLabel, null)) 
                });
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout()
            {
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

            Content = new ContentViewWithBottomBorder() { Content = absoluteLayout };
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
                var dialer = DependencyService.Get<IDialer>();

                phoneNumber = phoneNumber.Replace("-", "");

                if (dialer == null)
                    return;

                dialer.Dial(phoneNumber);
            }
        }
    }
}

