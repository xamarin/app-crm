using MobileCRM.Pages.Home;
using MobileCRM.Pages.Products;
using MobileCRM.Pages.Sales;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages
{
    public class RootPage : TabbedPage
    {
        public RootPage ()
        {
            MessagingCenter.Subscribe<ILogin>(this, MessagingServiceConstants.AUTHENTICATED, (sender) => Navigation.PopModalAsync());

            // the Sales tab page
            this.Children.Add (new SalesDashboardPage(new SalesDashboardViewModel(Navigation)) { Title = "Sales", });
            // the Customers tab page
            this.Children.Add (new ContentPage () { Title = "Customers" });
            // the Products tab page
            this.Children.Add (new NavigationPage(new CategoryListPage()) { Title = "Products" });
        }
    }
}

