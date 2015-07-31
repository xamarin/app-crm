using MobileCRM.Pages.Home;
using MobileCRM.Pages.Products;
using MobileCRM.Pages.Sales;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.ViewModels.Accounts;

namespace MobileCRM.Pages
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            // the Sales tab page
            this.Children.Add(
                new NavigationPage(new SalesDashboardPage() { BindingContext = new SalesDashboardViewModel() { Navigation = Navigation } })
                { 
                    Title = TextResources.MainTabs_Sales, 
                    Icon = new FileImageSource() { File = "SalesTab" }
                }
            );

            // the Customers tab page
            this.Children.Add(
                new CustomersPage()
                { 
                    BindingContext = new AccountsViewModel() { Navigation = Navigation }, 
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
    }
}

