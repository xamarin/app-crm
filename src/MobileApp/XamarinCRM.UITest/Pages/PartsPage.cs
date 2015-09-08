using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class PartsPage : BasePage
    {
        public PartsPage(IApp app, Platform platform)
            : base(app, platform, "Build Plates", "Parts")
        {
        }

        public void SelectPart(string partName)
        {
            app.Tap(partName);
        }
    }
}

