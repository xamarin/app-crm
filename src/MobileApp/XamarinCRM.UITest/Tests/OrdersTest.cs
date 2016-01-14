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
        public void EditCustomerOrder()
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
                .SelectColor("BLU-LIGHT");

            new ProductDetailsPage(app, platform)
                .AddToOrder();
            
            new CustomerOrderDetailsPage(app, platform)
                .ChangePrice(44)
                .SaveAndExit();
            //      .changeDate();
        }

        [Test]
        public void AddNewOrder()
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

        }

        [Test]
        public void DeliverOrder()
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
        public void AddNewOrderAndDeliver()
        {
            AddNewOrder();

            new CustomerOrdersPage(app, platform) 
                .SelectFirstOrder();

            new CustomerOrderDetailsPage(app, platform)
                .DeliverOrder();

            app.Screenshot("Order removed from list");
        }
    }
}
