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
            await App.ExecuteIfConnected(async () =>
                {
                    Account account = (Account)e.Item;
                    await PushTabbedPage(account);
                });
            #endregion

            #region compose view hierarchy
            Content = new UnspacedStackLayout()
            {
                Children =
                {
                    customerListActivityIndicator,
                    customerListView
                },
                Padding = Device.OnPlatform(Thicknesses.IosStatusBar, Thicknesses.Empty, Thicknesses.Empty)
            };
            #endregion
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

