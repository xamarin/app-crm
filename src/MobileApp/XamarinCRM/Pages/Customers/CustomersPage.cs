using Xamarin.Forms;
using Xamarin;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using System.Threading.Tasks;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Statics;
using XamarinCRM.Views.Customers;
using XamarinCRM.Converters;

namespace XamarinCRM.Pages.Customers
{
    public class CustomersPage : ModelBoundContentPage<CustomersViewModel>
    {
        public CustomersPage()
        {
            StackLayout stackLayout = new UnspacedStackLayout();

            #region customer list activity inidicator
            ActivityIndicator customerListActivityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.MediumRowHeight
            };
            customerListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            customerListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            customerListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region customer list
            CustomerListView customerListView = new CustomerListView();
            customerListView.SetBinding(CustomerListView.ItemsSourceProperty, "Accounts");
            customerListView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            customerListView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            customerListView.ItemTapped += async (sender, e) =>
            {
                Account account = (Account)e.Item;  // commented out temporarily
                await PushTabbedPage(account);
            };
            #endregion

            stackLayout.Children.Add(customerListActivityIndicator);

            stackLayout.Children.Add(customerListView);

            Device.OnPlatform(iOS: () => stackLayout.Padding = new Thickness(0, 20, 0, 0));

            Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Customers Page");

            if (ViewModel.IsInitialized)
            {
                return;
            }
            ViewModel.LoadAccountsCommand.Execute(null);
            ViewModel.IsInitialized = true;
        }

        async Task PushTabbedPage(Account account = null)
        {
            await ViewModel.PushModalAsync(new NavigationPage(new CustomerTabbedPage(ViewModel.Navigation, account)));
        }
    }
}

