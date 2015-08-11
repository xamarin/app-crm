using XamarinCRM.Pages.Customers;
using XamarinCRM.Pages.Products;
using XamarinCRM.Pages.Sales;
using XamarinCRM.Pages.Splash;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.ViewModels.Splash;
using Xamarin.Forms;

namespace XamarinCRM.Pages
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
                await Navigation.PushModalAsync(new SplashPage() { BindingContext = new SplashPageViewModel(Navigation) }, false); // setting false to create a smoother transition from loading screen to splash screen on iOS
            }
        }
    }
}

