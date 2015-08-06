using Xamarin.Forms;
using MobileCRM.Pages.Base;

namespace MobileCRM.Pages.Customers
{
    public class CustomerSalesPage : BaseCustomerDetailPage<CustomerSalesViewModel>
    {
        public CustomerSalesPage()
        {
            #region sales chart header
            #endregion

            #region sales chart
            #endregion

//            Content = StackLayout;

            RelativeLayout relativeLayout = new RelativeLayout();

            Label label = new Label()
                { 
                    Text = "Coming Soon!",
                    TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                    FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center
                };

            relativeLayout.Children.Add(label, widthConstraint: Constraint.RelativeToParent(parent => parent.Width), heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            Content = relativeLayout;
        }
    }
}

