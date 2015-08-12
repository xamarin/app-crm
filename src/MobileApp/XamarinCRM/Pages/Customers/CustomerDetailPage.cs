using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Pages.Base;
using XamarinCRM.Views.Base;
using System;
using XamarinCRM.Interfaces;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelTypedContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage()
        {
            #region header
            AbsoluteLayout headerAbsoluteLayout = new AbsoluteLayout() { HeightRequest = 150 };

            StackLayout headerLabelsStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Image companyImage = new Image() { Aspect = Aspect.AspectFill };
            companyImage.SetBinding(Image.SourceProperty, "Account.ImageUrl");

            Image gradientImage = new Image() { Aspect = Aspect.Fill, Source = new FileImageSource() { File = "bottom_up_gradient" }, HeightRequest = 75, BindingContext = companyImage };
            gradientImage.SetBinding(Image.IsVisibleProperty, "IsLoading", converter: new InvertedBooleanConverter());

            ActivityIndicator imageLoadingIndicator = new ActivityIndicator() { BindingContext = companyImage };
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");

            Label companyLabel = new Label()
            { 
                TextColor = Color.White, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyLabel.SetBinding(Label.TextProperty, "Account.Company");

            Label industryLabel = new Label()
            { 
                TextColor = Palette._016,
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");

            headerLabelsStackLayout.Children.Add(companyLabel);
            headerLabelsStackLayout.Children.Add(industryLabel);

            headerAbsoluteLayout.Children.Add(companyImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            headerAbsoluteLayout.Children.Add(imageLoadingIndicator, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            headerAbsoluteLayout.Children.Add(gradientImage, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            headerAbsoluteLayout.Children.Add(headerLabelsStackLayout, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            #endregion

            #region contact
            StackLayout contactLabelsStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label contactTitleLabel = new Label()
            { 
                Text = TextResources.Contact,
                TextColor = Device.OnPlatform(Palette._007, Color.Gray, Color.Gray),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label contactLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            contactLabel.SetBinding(Label.TextProperty, "Account.DisplayContact");

            contactLabelsStackLayout.Children.Add(contactTitleLabel);
            contactLabelsStackLayout.Children.Add(contactLabel);
            #endregion

            #region phone
            StackLayout phoneLabelStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label phoneTitleLabel = new Label()
            { 
                Text = TextResources.Phone,
                TextColor = Device.OnPlatform(Palette._007, Color.Gray, Color.Gray),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label phoneLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            phoneLabel.SetBinding(Label.TextProperty, "Account.Phone");

            phoneLabel.GestureRecognizers.Add(new TapGestureRecognizer(){ Command = new Command(x => OnPhoneTapped(phoneLabel, null)) });

            phoneLabelStackLayout.Children.Add(phoneTitleLabel);
            phoneLabelStackLayout.Children.Add(phoneLabel);
            #endregion

            #region address
            StackLayout addressLabelStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label addressTitleLabel = new Label()
            { 
                Text = TextResources.Customers_Detail_Address,
                TextColor = Device.OnPlatform(Palette._007, Color.Gray, Color.Gray),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label addressStreetLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStreetLabel.SetBinding(Label.TextProperty, "Account.Street");

            Label addressCityLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressCityLabel.SetBinding(Label.TextProperty, "Account.City");

            Label addressStatePostalLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStatePostalLabel.SetBinding(Label.TextProperty, "Account.StatePostal");

            addressLabelStackLayout.Children.Add(addressTitleLabel);
            addressLabelStackLayout.Children.Add(addressStreetLabel);
            addressLabelStackLayout.Children.Add(addressCityLabel);
            addressLabelStackLayout.Children.Add(addressStatePostalLabel);

            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(headerAbsoluteLayout);
            stackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = contactLabelsStackLayout });
            stackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = phoneLabelStackLayout });
            stackLayout.Children.Add(addressLabelStackLayout);

            Content = new ScrollView() { Content = stackLayout };
        }

        async void OnPhoneTapped(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            string phoneNumber = ((Label)sender).Text;

            if (String.IsNullOrWhiteSpace(phoneNumber))
                return;        

            if (await DisplayAlert(
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

