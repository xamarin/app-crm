using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;
using XamarinCRM.Pages.Base;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelTypedContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            BindingContext = viewModel;

            StackLayout stackLayout = new UnspacedStackLayout();

            // add header view
            stackLayout.Children.Add(new CustomerDetailHeaderView() { BindingContext = ViewModel });

            // add contact view
            stackLayout.Children.Add(new CustomerDetailContactView() { BindingContext = ViewModel });

            // add phone number view
            stackLayout.Children.Add(new CustomerDetailPhoneView(this) { BindingContext = ViewModel });

            // add address view
            stackLayout.Children.Add(new CustomerDetailAddressView() { BindingContext = ViewModel });

            Content = new ScrollView() { Content = stackLayout };
        }
    }
}

