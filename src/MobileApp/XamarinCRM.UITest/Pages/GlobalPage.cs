using System;
using System.Threading;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class GlobalPage : BasePage
    {
        readonly string BackButton;
        readonly string SalesTab;
        readonly string CustomersTab;
        readonly string ProductsTab;
        readonly Query MenuFlyout;

        public GlobalPage(IApp app, Platform platform)
            : base(app, platform)
        {
            if (OnAndroid)
            {
                SalesTab = "Sales";
                CustomersTab = "Customers";
                ProductsTab = "Products";
                MenuFlyout = x => x.Class("android.widget.ImageButton").Marked("OK");
            }
            if (OniOS)
            {
                BackButton = "Back";
                SalesTab = "Sales";
                CustomersTab = "Customers";
                ProductsTab = "Products";
            }
        }

        public void GoBack()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (OniOS)
                app.Tap(BackButton);
            if (OnAndroid)
                app.Back();

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public void NavigateToSales()
        {
            if (OnAndroid)
                app.Tap(MenuFlyout);
            app.Tap(SalesTab);
        }

        public void NavigateToCustomers()
        {
            if (OnAndroid)
                app.Tap(MenuFlyout);
            app.Tap(CustomersTab);
        }

        public void NavigateToProducts()
        {
            if (OnAndroid)
                app.Tap(MenuFlyout);
            app.Tap(ProductsTab);
        }
    }
}

