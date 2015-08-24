using Foundation;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin;

namespace XamarinCRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.

            #if DEBUG
            Xamarin.Calabash.Start();
            #endif

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // SQLite initialization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initialization
            Forms.Init();

            // Xamarin.Forms.Maps initialization
            FormsMaps.Init();

            // Bootstrap the "core" Xamarin.Forms app
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}