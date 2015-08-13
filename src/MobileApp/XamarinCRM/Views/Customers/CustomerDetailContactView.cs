using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM.Views.Customers
{
    public class CustomerDetailContactView : ModelTypedContentView<CustomerDetailViewModel>
    {
        public CustomerDetailContactView()
        {
            StackLayout stackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label contactTitleLabel = new Label()
            { 
                Text = TextResources.Contact,
                TextColor = Device.OnPlatform(Palette._007, Palette._009, Palette._008),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label contactLabel = new Label()
            { 
                TextColor = Palette._008, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            contactLabel.SetBinding(Label.TextProperty, "Account.DisplayContact");

            stackLayout.Children.Add(contactTitleLabel);
            stackLayout.Children.Add(contactLabel);

            Content = new ContentViewWithBottomBorder() { Content = stackLayout };
        }
    }
}

