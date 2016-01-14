using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ProductDetailsPage : BasePage
    {
        readonly Query AddToOrderButton;

        public ProductDetailsPage(IApp app, Platform platform)
            : base(app, platform, "content", "Back")
        {
            if (OnAndroid)
            {
                AddToOrderButton = x => x.Marked("Add to Order").Parent(0).Sibling();
            }
            if (OniOS)
            {
                AddToOrderButton = x => x.Id("add_ios_blue");
            }
        }

        public void VerifyProduct()
        {
            //TODO Add logic
            app.Screenshot("Product Details Page");
        }

        public void AddToOrder()
        {
            app.Tap(AddToOrderButton);
        }
    }
}

