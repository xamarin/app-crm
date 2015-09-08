using System;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class CustomerSalesPage : BasePage
    {
        readonly Query CustomerOrders = x => x.Marked("Orders");
        readonly Query CustomerContact = x => x.Marked("Customer");

        public CustomerSalesPage(IApp app, Platform platform)
            : base(app, platform)
        {
            if (OnAndroid)
                app.WaitForElement("WEEKLY AVERAGE", timeout: TimeSpan.FromMinutes(2));
            if (OniOS)
                app.WaitForElement("WEEKLY AVERAGE");

            app.Screenshot("On " + this.GetType().Name);
        }


        public void NavigateToCustomerOrders()
        {
            app.Tap(CustomerOrders);
        }

        public void NavigateToCustomerContact()
        {
            app.Tap(CustomerContact);
        }
    }
}

