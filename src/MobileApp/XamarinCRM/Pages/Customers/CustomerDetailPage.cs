using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;
using XamarinCRM.Pages.Base;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage()
        {

            StackLayout stackLayout = new UnspacedStackLayout()
            {
                Children =
                {
                    new CustomerDetailHeaderView(),
                    new CustomerDetailContactView(),
                    new CustomerDetailPhoneView(this),
                    new CustomerDetailAddressView()
                }
            };

            Content = new ScrollView() { Content = stackLayout };
        }
    }
}

