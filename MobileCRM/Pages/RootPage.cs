using MobileCRM.Pages.Home;
using MobileCRM.Pages.Products;
using MobileCRM.Pages.Sales;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MobileCRM.Pages
{
    public class RootPage : TabbedPage
    {
        public IPlatformParameters PlatformParameters { get; set; }

        public RootPage()
        {
            // the Sales tab page
            this.Children.Add(new SalesDashboardPage(new SalesDashboardViewModel(Navigation)) { Title = "Sales", Icon = new FileImageSource() { File = "SalesTab" } });

            // the Customers tab page
            this.Children.Add(new ContentPage() { Title = "Customers", Icon = new FileImageSource() { File = "CustomersTab" } });

            // the Products tab page
            this.Children.Add(new NavigationPage(new CategoryListPage()) { Title = "Products", Icon = new FileImageSource() { File = "ProductsTab" } });
        }
    }
}

