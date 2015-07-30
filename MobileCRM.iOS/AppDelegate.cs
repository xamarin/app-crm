using Microsoft.WindowsAzure.MobileServices;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;

namespace MobileCRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new SfChartRenderer();

            #if DEBUG
            Xamarin.Calabash.Start();
            #endif

            // Azure Mobile Services initilization
            CurrentPlatform.Init();

            // SQLite initilization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initilization
            global::Xamarin.Forms.Forms.Init();

            // Bootstrap the PCL app
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}