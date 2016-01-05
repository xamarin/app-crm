using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class CustomersPage : BasePage
    {
        readonly Query FirstContact;

        public CustomersPage(IApp app, Platform platform)
            : base(app, platform, "Customers", "Customers")
        {
            if (OnAndroid)
                FirstContact = x => x.Class("LabelRenderer").Index(0).Child(0);
            if (OniOS)
                FirstContact = x => x.Class("UITableViewCellContentView").Index(0).Descendant(4);   
        }

        public void ClickFirstContact()
        {
            app.Tap(FirstContact);
        }

        public void ClickContact(string contact)
        {
            app.Tap(contact);
        }

    }
}

