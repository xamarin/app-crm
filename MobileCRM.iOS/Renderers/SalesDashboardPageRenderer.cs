using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MobileCRM.Pages.Sales;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.iOS;

[assembly: ExportRenderer(typeof(SalesDashboardPage), typeof(SalesDashboardPageRenderer))]

namespace MobileCRM.iOS
{
    public class SalesDashboardPageRenderer : PageRenderer
    {
        SalesDashboardPage _Page;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            _Page = e.NewElement as SalesDashboardPage;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _Page.PlatformParameters = new PlatformParameters(this);
        }
    }
}

