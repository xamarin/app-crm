using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MobileCRM.Pages.Sales;
using MobileCRMAndroid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;

[assembly: ExportRenderer(typeof(SalesDashboardPage), typeof(SalesDashboardPageRenderer))]

namespace MobileCRMAndroid
{
    public class SalesDashboardPageRenderer : PageRenderer
    {
        SalesDashboardPage _Page;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            _Page = e.NewElement as SalesDashboardPage;

            _Page.PlatformParameters = new PlatformParameters((Activity)this.Context);
        }
    }
}

