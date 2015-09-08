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

        public GlobalPage(IApp app, Platform platform)
            : base(app, platform)
        {
            if (OnAndroid)
            {
                SalesTab = "Sales";
                CustomersTab = "Customers";
                ProductsTab = "Products";
            }
            if (OniOS)
            {
                BackButton = "Back";
                SalesTab = "SalesTab";
                CustomersTab = "CustomersTab";
                ProductsTab = "ProductsTab";
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
            app.Tap(SalesTab);
        }

        public void NavigateToCustomers()
        {
            app.Tap(CustomersTab);
        }

        public void NavigateToProducts()
        {
            app.Tap(ProductsTab);
        }
    }
}

