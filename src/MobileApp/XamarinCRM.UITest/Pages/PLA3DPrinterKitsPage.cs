using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class PLA3DPrinterKitsPage : BasePage
    {
        public PLA3DPrinterKitsPage(IApp app, Platform platform)
            : base(app, platform, "PLA-JEWEL", "PLA 3D Printer Kits")
        {
        }

        public void SelectItem(string itemName)
        {
            var itemChosen = string.Format("PLA-{0}", itemName);
            app.ScrollDownTo(itemChosen);
            app.Tap(itemChosen);
        }
    }
}

