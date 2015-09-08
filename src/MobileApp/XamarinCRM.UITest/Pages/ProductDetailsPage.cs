using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ProductDetailsPage : BasePage
    {
        readonly Query AddToOrderButton = x => x.Marked("ADD TO ORDER");

        public ProductDetailsPage(IApp app, Platform platform)
            : base(app, platform, "action_bar_container", "Back")
        {
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

