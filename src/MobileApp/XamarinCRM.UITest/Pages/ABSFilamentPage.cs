using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ABSFilamentPage : BasePage
    {
        public ABSFilamentPage(IApp app, Platform platform)
            : base(app, platform, "FIL-ABS-BLU", "ABS Filament")
        {
        }

        public void SelectColor(string color)
        {
            var colorChosen = string.Format("FIL-ABS-{0}", color);
            app.ScrollDownTo(colorChosen);
            app.Tap(colorChosen);
        }
    }
}

