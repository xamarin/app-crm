using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class ProductTests : AbstractSetup
    {
        public ProductTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void partsLoadCheck()
        {
            new GlobalPage(app, platform)
                .NavigateToProducts();

            new ProductsPage(app, platform)
                .SelectProduct("Parts");

            new PartsPage(app, platform)
                .SelectPart("Build Plates");

            new BuildPlatesPage(app, platform)
                .SelectItem("BLD-PLT-ABS");

            new ProductDetailsPage(app, platform)
                .VerifyProduct();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new ProductsPage(app, platform);
        }

        [Test]
        public void extruderCheck()
        {
            int num = 2;

            new GlobalPage(app, platform)
                .NavigateToProducts();

            new ProductsPage(app, platform)
                .SelectProduct("Parts");

            new PartsPage(app, platform)
                .SelectPart("Extruders");

            new ExtruderPage(app, platform)
                .SelectItem(num);

            new ProductDetailsPage(app, platform)
                .VerifyProduct();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new ProductsPage(app, platform);
        }

        [Test]
        public void printerKitsCheck()
        {
            string num = "HOBY";

            new GlobalPage(app, platform)
                .NavigateToProducts();

            new ProductsPage(app, platform)
                .SelectProduct("3D Printer Kits");

            new ThreeDPrinterKitsPage(app, platform)
                .SelectPart("PLA 3D Printer Kits");

            new PLA3DPrinterKitsPage(app, platform)
                .SelectItem(num);

            new ProductDetailsPage(app, platform)
                .VerifyProduct();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new GlobalPage(app, platform)
                .GoBack();

            new ProductsPage(app, platform);
        }
    }
}
