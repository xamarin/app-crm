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

            StackLayout phoneLabelStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label phoneTitleLabel = new Label()
            { 
                Text = TextResources.Phone,
                TextColor = Device.OnPlatform(Palette._007, Palette._008, Palette._008),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label phoneLabel = new Label()
            { 
                TextColor = Palette._009, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            phoneLabel.SetBinding(Label.TextProperty, "Account.Phone");
            phoneLabel.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => OnPhoneTapped(phoneLabel, null)) });

            Image phoneImage = new Image() { Source = new FileImageSource { File = Device.OnPlatform("phone_ios", "phone_android", null) }, Aspect = Aspect.AspectFit }; 

            phoneLabelStackLayout.Children.Add(phoneTitleLabel);
            phoneLabelStackLayout.Children.Add(phoneLabel);

            Content = new ContentViewWithBottomBorder() { Content = phoneLabelStackLayout };
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

