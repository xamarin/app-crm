using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Syncfusion.SfChart.XForms.Droid;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCRM;
using HockeyApp.Android;

namespace XamarinCRMAndroid
{
	[Activity(Label = "XamarinCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// Set the HockeyApp App Id here:
			CrashManager.Register(this, "11111111222222223333333344444444"); // This is just a dummy value. Replace with your real HockeyApp App ID

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


