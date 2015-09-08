using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IApp app, Platform platform)
            : base(app, platform, "Products", "Products")
        {
        }

        public void SelectProduct(string productName)
        {
            app.Tap(productName);

        }
    }
}

