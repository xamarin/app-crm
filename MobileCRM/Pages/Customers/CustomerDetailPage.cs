using Xamarin.Forms;
using MobileCRM.Layouts;
using MobileCRM.Statics;
using MobileCRM.ViewModels.Customers;
using MobileCRM.Pages.Base;

namespace MobileCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelTypedContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage()
        {
            #region header
            var headerContainer = new UnspacedStackLayout();

            var headerLabelsStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            var companyLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyLabel.SetBinding(Label.TextProperty, "Account.Company");

            var industryLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Gray, Color.Gray, Color.Gray),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");

            headerLabelsStackLayout.Children.Add(companyLabel);
            headerLabelsStackLayout.Children.Add(industryLabel);
            headerContainer.Children.Add(headerLabelsStackLayout);
            #endregion

            #region contact
            var contactContainer = new UnspacedStackLayout();

            var contactLabelsStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            var contactTitleLabel = new Label()
            { 
                Text = TextResources.Contact,
                TextColor = Device.OnPlatform(Color.Gray, Color.Gray, Color.Gray),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            var contactLabel = new Label()
            { 
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            contactLabel.SetBinding(Label.TextProperty, "Account.DisplayContact");

            contactLabelsStackLayout.Children.Add(contactTitleLabel);
            contactLabelsStackLayout.Children.Add(contactLabel);
            contactContainer.Children.Add(contactLabelsStackLayout);
            #endregion

            #region phone
            var phoneContainer = new UnspacedStackLayout();

            var phoneLabelStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            var phoneTitleLabel = new Label()
                { 
                    Text = TextResources.Phone,
                    TextColor = Device.OnPlatform(Color.Gray, Color.Gray, Color.Gray),
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                    LineBreakMode = LineBreakMode.TailTruncation
                };

            var phoneLabel = new Label()
                { 
                    TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                    LineBreakMode = LineBreakMode.TailTruncation
                };
            phoneLabel.SetBinding(Label.TextProperty, "Account.Phone");

            phoneLabelStackLayout.Children.Add(phoneTitleLabel);
            phoneLabelStackLayout.Children.Add(phoneLabel);
            phoneContainer.Children.Add(phoneLabelStackLayout);
            #endregion

            #region address

            var addressContainer = new UnspacedStackLayout();

            var addressLabelStackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            var addressTitleLabel = new Label()
                { 
                    Text = TextResources.Customers_Detail_Address,
                    TextColor = Device.OnPlatform(Color.Gray, Color.Gray, Color.Gray),
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                    LineBreakMode = LineBreakMode.TailTruncation
                };

            var addressStreetLabel = new Label()
                { 
                    TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                    LineBreakMode = LineBreakMode.TailTruncation
                };
            addressStreetLabel.SetBinding(Label.TextProperty, "Account.Street");

            var addressCityLabel = new Label()
                { 
                    TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), 
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                    LineBreakMode = LineBreakMode.TailTruncation
                };
            addressCityLabel.SetBinding(Label.TextProperty, "Account.City");

            var addressStatePostalLabel = new Label()
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
            addressContainer.Children.Add(addressLabelStackLayout);

            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(new ContentViewWithBottomBorder(){ Content = headerContainer });
            stackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = contactContainer });
            stackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = phoneContainer });
            stackLayout.Children.Add(addressContainer);

            Content = new ScrollView() { Content = stackLayout };
        }

        private StackLayout GetSeparator()
        {
            return new UnspacedStackLayout() { HeightRequest = 1, BackgroundColor = Palette._014 };
        }
    }
}

