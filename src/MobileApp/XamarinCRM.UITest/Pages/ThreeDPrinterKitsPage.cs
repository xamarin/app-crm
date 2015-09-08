using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ThreeDPrinterKitsPage : BasePage
    {
        public ThreeDPrinterKitsPage(IApp app, Platform platform)
            : base(app, platform, "ABS 3D Printer Kits", "3D Printer Kits")
        {
        }

        public void SelectPart(string partName)
        {
            app.Tap(partName);
        }
    }
}

