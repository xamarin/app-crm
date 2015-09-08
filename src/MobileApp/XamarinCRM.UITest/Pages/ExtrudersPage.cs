using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ExtruderPage : BasePage
    {
        public ExtruderPage(IApp app, Platform platform)
            : base(app, platform, "EXTR-001", "Extruders")
        {
        }

        public void SelectItem(int itemNumber)
        {
            var itemChosen = string.Format("EXTR-00{0}", itemNumber);
            app.ScrollDownTo(itemChosen);
            app.Tap(itemChosen);
        }
    }
}

