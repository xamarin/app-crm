using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class CoolingFansPage : BasePage
    {
        public CoolingFansPage(IApp app, Platform platform)
            : base(app, platform, "FAN-003", "Cooling Fans")
        {
        }

        public void SelectItem(int itemNumber)
        {
            var itemChosen = string.Format("FAN-00{0}", itemNumber);
            app.ScrollTo(itemChosen);
            app.Tap(itemChosen);
        }
    }
}

