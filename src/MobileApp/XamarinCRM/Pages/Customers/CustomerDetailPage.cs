using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;
using XamarinCRM.Pages.Base;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            BindingContext = viewModel;

            StackLayout stackLayout = new UnspacedStackLayout()
            {
                Children =
                {
                    new CustomerDetailHeaderView() { BindingContext = ViewModel },
                    new CustomerDetailContactView() { BindingContext = ViewModel },
                    new CustomerDetailPhoneView(this) { BindingContext = ViewModel },
                    new CustomerDetailAddressView() { BindingContext = ViewModel }
                }
            };

            Content = new ScrollView() { Content = stackLayout };
        }
    }
}

