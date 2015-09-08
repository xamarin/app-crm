using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ABS3DPrinterKitsPage : BasePage
    {
        public ABS3DPrinterKitsPage(IApp app, Platform platform)
            : base(app, platform, "ABS-CELL", "ABS 3D Printer Kits")
        {
        }

        public void SelectItem(string itemName)
        {
            var itemChosen = string.Format("ABS-{0}", itemName);
            app.ScrollTo(itemChosen);
            app.Tap(itemChosen);
        }
    }
}

