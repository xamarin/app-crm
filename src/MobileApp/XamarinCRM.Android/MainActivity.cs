using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Syncfusion.SfChart.XForms.Droid;
using System;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCRM;

namespace XamarinCRMAndroid
{
    [Activity(Label = "XamarinCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public const string HOCKEYAPP_APPID = "f624832e5ecae02ea81b61a11a6c1248";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HOCKEYAPP_APPID);

            //Register to with the Update Manager
            HockeyApp.UpdateManager.Register(this, HOCKEYAPP_APPID);

            // Initialize the Trace Writer
            HockeyApp.TraceWriter.Initialize();

            // Wire up Unhandled Expcetion handler from Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                // Use the trace writer to log exceptions so HockeyApp finds them
                HockeyApp.TraceWriter.WriteTrace(args.Exception);
                args.Handled = true;
            };

            // Wire up the .NET Unhandled Exception handler
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

            // Wire up the unobserved task exception handler
            TaskScheduler.UnobservedTaskException +=
                (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);

            FormsMaps.Init(this, bundle);
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.
            ImageCircleRenderer.Init();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}


