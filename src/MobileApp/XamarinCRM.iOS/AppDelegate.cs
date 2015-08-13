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
            new SfChartRenderer();

            #if DEBUG
            Xamarin.Calabash.Start();
            #endif

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // SQLite initilization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initilization
            Forms.Init();

            // Xamarin.Forms.Maps initilization
            FormsMaps.Init();

            // Bootstrap the PCL app
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}