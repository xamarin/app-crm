using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class BuildPlatesPage : BasePage
    {
        public BuildPlatesPage(IApp app, Platform platform)
            : base(app, platform, "BLD-PLT-PLA", "Build Plates")
        {
        }

        public void SelectItem(string itemName)
        {
            app.Tap(itemName);
        }
    }
}

