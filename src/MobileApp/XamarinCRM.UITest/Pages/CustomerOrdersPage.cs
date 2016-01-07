using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class CustomerOrdersPage : BasePage
    {
        readonly Query CustomerContact = x => x.Marked("Customer");
        readonly Query CustomerSales = x => x.Marked("Sales");
        readonly Query NewOrderButton;
        readonly Query SecondOrder;

        public CustomerOrdersPage(IApp app, Platform platform)
            : base(app, platform, "Open Orders", "Open Orders")
        {
            if (OnAndroid)
            {
                SecondOrder = x => x.Class("LabelRenderer").Index(1);
                NewOrderButton = x => x.Class("FloatingActionButton");
            }
            if (OniOS)
            {
                SecondOrder = x => x.Class("UITableViewCellContentView").Index(1);
                NewOrderButton = x => x.Marked("add_ios_gray");
            }
        }

        public void NavigateToCustomerContact()
        {
            app.Tap(CustomerContact);
        }

        public void NavigateToCustomerSales()
        {
            app.Tap(CustomerSales);
        }

        public void SelectFirstOrder()
        {
            app.Tap(SecondOrder);
        }

        public void AddNewOrder()
        {
            app.Tap(NewOrderButton);
        }
    }
}

