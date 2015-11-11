using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class FilamentsTest : AbstractSetup
    {
        public FilamentsTest(Platform platform)
            : base(platform)
        {
        }

        [Category("sanity")]
        [Test]
        public void SelectingFilamentColor()
        {
            string color = "YLW";

            new GlobalPage(app, platform)
                .NavigateToProducts();

            new ProductsPage(app, platform)
                .SelectProduct("3D Filament");

            new ThreeDFilamentPage(app, platform)
                .SelectPart("PLA Filament");

            new PLAFilamentPage(app, platform)
                .SelectColor(color);

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new ThreeDFilamentPage(app, platform)
                .SelectPart("ABS Filament");

            new ABSFilamentPage(app, platform)
                .SelectColor(color);
        }

        [Category("sanity")]
        [Test]
        public void AppLaunches()
        {
            app.Screenshot("App launches. XTC is up");
        }
    }
}

