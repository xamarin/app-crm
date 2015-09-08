using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class ThreeDFilamentPage : BasePage
    {
        public ThreeDFilamentPage(IApp app, Platform platform)
            : base(app, platform, "PLA Filament", "3D Filament")
        {
        }

        public void SelectPart(string partName)
        {
            app.Tap(partName);
        }
    }
}

