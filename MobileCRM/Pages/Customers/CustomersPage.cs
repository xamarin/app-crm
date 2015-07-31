using System;
using Xamarin.Forms;
using MobileCRM.ViewModels.Accounts;
using Xamarin;
using MobileCRM.Cells;
using MobileCRM.Customers;
using MobileCRM.Layouts;
using MobileCRM.Models;
using System.Threading.Tasks;
using MobileCRM.Pages.Base;

namespace MobileCRM.Pages.Customers
{
    public class CustomersPage : ModelEnforcedContentPage<AccountsViewModel>
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
            customerListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            customerListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region customer list
            CustomerListView customerListView = new CustomerListView()
            {
                ItemTemplate = new DataTemplate(typeof(CustomerListItemCell)), 
                SeparatorColor = Palette._013
                
            };
            customerListView.SetBinding(CustomerListView.ItemsSourceProperty, "Accounts");
            customerListView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            customerListView.SetBinding(IsVisibleProperty, "IsModelLoaded");

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

        protected override async void OnAppearing()
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
            AccountDetailsViewModel viewModel = new AccountDetailsViewModel(account) { Navigation = ViewModel.Navigation }; 

            TabbedPage tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new CustomerDetailPage()
                {
                    Title = TextResources.Customers_Detail_Tab_Title,
                    BindingContext = viewModel,
                    Icon = new FileImageSource() { File = "CustomersTab" },

                });

            tabbedPage.Children.Add(new CustomerOrdersPage()
                {
                    Title = TextResources.Customers_Orders_Tab_Title,
                    BindingContext = viewModel,
                    Icon = new FileImageSource() { File = "ProductsTab" }
                });

            tabbedPage.Children.Add(new CustomerSalesPage()
                {
                    Title = TextResources.Customers_Sales_Tab_Title,
                    BindingContext = viewModel,
                    Icon = new FileImageSource() { File = "SalesTab" }
                });

            await ViewModel.PushModalAsync(tabbedPage);
        }
    }
}

