using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Threading;

namespace XamarinCRM.UITest
{
    public class CustomerContactPage : BasePage
    {
        protected string CustomerOrders = "Orders";
        protected string CustomerSales = "Sales";
        protected string DialCancelButton = "No";

        protected Query PhoneButton;
        protected Query MapButton;
        protected Query HeaderImage;

        public CustomerContactPage(IApp app, Platform platform)
            : base(app, platform, "Address", "Address")
        {
            if (OnAndroid)
            {
                PhoneButton = x => x.Class("FormsImageView").Index(2);
                MapButton = x => x.Class("FormsImageView").Index(3);
                HeaderImage = x => x.Class("ImageRenderer").Index(0);
            }

            if (OniOS)
            {
                PhoneButton = x => x.Marked("phone_ios");
                MapButton = x => x.Marked("map_marker_ios");
                HeaderImage = x => x.Class("Xamarin_Forms_Platform_iOS_ImageRenderer").Index(0);
            }
        }

        public CustomerContactPage VerifyContactInfoPresent()
        {
            app.WaitForElement("Contact", timeoutMessage: "Contact info not present");
            app.WaitForElement("Phone", timeoutMessage: "Phone info not present");
            app.WaitForElement("Address", timeoutMessage: "Address info not present");

            return this;
        }

        public void NavigateToCustomerOrders()
        {
            if (OniOS)
            {
                app.ScrollDownTo(CustomerOrders);
            }

            app.WaitForElement(HeaderImage);
            Thread.Sleep(3000);
            app.Tap(CustomerOrders);
        }

        public void NavigateToCustomerSales()
        {
            if (OniOS)
            {
                app.ScrollDownTo(CustomerSales);
            }
            app.Tap(CustomerSales);
        }

        public void CheckPhone()
        {
            app.Tap(PhoneButton);
            app.Screenshot("Open phone");
            app.Tap(DialCancelButton);
            app.Screenshot("Cancel call");
            app.Tap(PhoneButton);
            app.Screenshot("Open phone again");
        }

        public void CheckNavigation()
        {
            app.Tap(MapButton);
            app.Screenshot("Open maps");
        }
    }
}

