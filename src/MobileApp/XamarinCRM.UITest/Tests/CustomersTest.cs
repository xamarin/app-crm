using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class CustomersTest : AbstractSetup
    {
        public CustomersTest(Platform platform)
            : base(platform)
        {
        }

        [Category("TestLogin")]
        [Test]
        public void CheckCustomerDetails()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .VerifyContactInfoPresent();
        }

        [Test]
        [Category("wall")]
        public void InvestigateCustomerPage()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .NavigateToCustomerOrders();

            new CustomerOrdersPage(app, platform)
                .NavigateToCustomerSales();

            new CustomerSalesPage(app, platform);
        }

        [Test]
        public void CheckCustomerPhone()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .CheckPhone();
        }

        [Test]
        public void CheckCustomerNavigation()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .CheckNavigation();
        }
    }
}

