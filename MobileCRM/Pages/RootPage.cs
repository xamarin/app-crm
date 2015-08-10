using MobileCRM.Pages.Customers;
using MobileCRM.Pages.Products;
using MobileCRM.Pages.Sales;
using MobileCRM.Pages.Splash;
using MobileCRM.ViewModels.Customers;
using MobileCRM.ViewModels.Sales;
using MobileCRM.ViewModels.Splash;
using Xamarin.Forms;

namespace MobileCRM.Pages
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            // the Sales tab page
            this.Children.Add(
                new NavigationPage(new SalesDashboardPage() { BindingContext = new SalesDashboardViewModel(Navigation) })
                { 
                    Title = TextResources.MainTabs_Sales, 
                    Icon = new FileImageSource() { File = "SalesTab" }
                }
            );

            // the Customers tab page
            this.Children.Add(
                new CustomersPage()
                { 
                    BindingContext = new CustomersViewModel(Navigation), 
                    Title = TextResources.MainTabs_Customers, 
                    Icon = new FileImageSource() { File = "CustomersTab" } 
                }
            );

            // the Products tab page
            this.Children.Add(
                new NavigationPage(new CategoryListPage())
                { 
                    Title = TextResources.MainTabs_Products, 
                    Icon = new FileImageSource() { File = "ProductsTab" } 
                }
            );
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // If the App.IsAuthenticated property is false, modally present the SplashPage.
            if (!App.IsAuthenticated)
            {
                await Navigation.PushModalAsync(new SplashPage() { BindingContext = new SplashPageViewModel(Navigation) });
            }
        }
    }
}

