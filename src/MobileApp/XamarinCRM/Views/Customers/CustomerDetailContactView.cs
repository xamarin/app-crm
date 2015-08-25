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
                TextColor = Device.OnPlatform(Palette._005, Palette._007, Palette._006),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
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

