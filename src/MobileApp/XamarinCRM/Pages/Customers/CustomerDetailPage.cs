using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ContentPage
    {
        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            StackLayout stackLayout = new UnspacedStackLayout();

            // add header view
            stackLayout.Children.Add(new CustomerDetailHeaderView() { BindingContext = viewModel });

            // add contact view
            stackLayout.Children.Add(new CustomerDetailContactView() { BindingContext = viewModel });

            // add phone number view
            stackLayout.Children.Add(new CustomerDetailPhoneView(this) { BindingContext = viewModel });

            // add address view
            stackLayout.Children.Add(new CustomerDetailAddressView() { BindingContext = viewModel });

            Content = new ScrollView() { Content = stackLayout };
        }
    }
}

