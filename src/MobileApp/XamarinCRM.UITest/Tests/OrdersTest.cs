using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class OrderTests : AbstractSetup
    {
        public OrderTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void editCustomerOrder()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .NavigateToCustomerOrders();

            new CustomerOrdersPage(app, platform) 
                .SelectFirstOrder();

            new CustomerOrderDetailsPage(app, platform)
                .ChangeProduct();

            new ProductsPage(app, platform)
                .SelectProduct("3D Filament");

            new ThreeDFilamentPage(app, platform)
                .SelectPart("ABS Filament");

            new ABSFilamentPage(app, platform)
                .SelectColor("VLT");

            new ProductDetailsPage(app, platform)
                .AddToOrder();

            new CustomerOrderDetailsPage(app, platform)
                .ChangePrice(44.05)
                .SaveAndExit();
            //      .changeDate();

            app.ScrollDownTo("FIL-ABS-VLT");
        }

        [Test]
        public void addNewOrder()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .NavigateToCustomerOrders();

            new CustomerOrdersPage(app, platform) 
                .AddNewOrder();

            new CustomerOrderDetailsPage(app, platform)
                .ChangeProduct();

            new ProductsPage(app, platform)
                .SelectProduct("3D Printer Kits");

            new ThreeDPrinterKitsPage(app, platform)
                .SelectPart("PLA 3D Printer Kits");

            new PLA3DPrinterKitsPage(app, platform)
                .SelectItem("DELIKT");

            new ProductDetailsPage(app, platform)
                .AddToOrder();

            new CustomerOrderDetailsPage(app, platform)
                .SaveAndExit();
            //      .changeDate();

            app.ScrollDownTo("PLA-DELIKT");
        }

        [Test]
        public void deliverOrder()
        {
            new GlobalPage(app, platform)
                .NavigateToCustomers();

            new CustomersPage(app, platform)
                .ClickFirstContact();

            new CustomerContactPage(app, platform)
                .NavigateToCustomerOrders();

            new CustomerOrdersPage(app, platform) 
                .SelectFirstOrder();

            new CustomerOrderDetailsPage(app, platform)
                .DeliverOrder();

            app.Screenshot("Order removed from list");
        }

        [Test]
        public void addNewOrderAndDeliver()
        {
            addNewOrder();

            new CustomerOrdersPage(app, platform) 
                .SelectFirstOrder();

            new CustomerOrderDetailsPage(app, platform)
                .DeliverOrder();

            app.Screenshot("Order removed from list");
        }
    }
}
