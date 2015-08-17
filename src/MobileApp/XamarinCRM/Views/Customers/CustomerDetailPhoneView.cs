using System;
using Xamarin.Forms;
using XamarinCRM.Interfaces;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM
{
    public class CustomerDetailPhoneView : ModelTypedContentView<CustomerDetailViewModel>
    {
        readonly Page _Page;

        public CustomerDetailPhoneView(Page page)
        {
            _Page = page;

            StackLayout stackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label phoneTitleLabel = new Label()
            { 
                Text = TextResources.Phone,
                TextColor = Device.OnPlatform(Palette._007, Palette._009, Palette._008),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label phoneLabel = new Label()
            { 
                TextColor = Palette._008, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            phoneLabel.SetBinding(Label.TextProperty, "Account.Phone");

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

            stackLayout.Children.Add(phoneTitleLabel);
            stackLayout.Children.Add(phoneLabel);

            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);

            absoluteLayout.Children.Add(phoneImageTouchView, new Rectangle(.95, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);

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
                    title: "Dial a Number",
                    message: "Would you like to call " + phoneNumber + "?",
                    accept: "Yes",
                    cancel: "No"))
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

