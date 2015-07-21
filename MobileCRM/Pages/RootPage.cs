using System;
using Xamarin.Forms;
using MobileCRM.Pages.Sales;
using MobileCRM.ViewModels.Home;
using MobileCRM.Pages.Products;
using MobileCRM.Pages.Home;

namespace MobileCRM
{
    public class RootPage : TabbedPage
    {
        public RootPage ()
        {
            MessagingCenter.Subscribe<ILogin>(this, "Authenticated", (sender) => Navigation.PopModalAsync());

            this.Children.Add (new SalesDashboardPage(new DashboardViewModel() { Navigation = Navigation }) { Title = "Sales", });
            this.Children.Add (new ContentPage () { Title = "Customers" });
            this.Children.Add (new NavigationPage(new CategoryListPage()) { Title = "Products" });
        }
    }
}

