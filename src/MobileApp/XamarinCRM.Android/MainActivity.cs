using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin;
using Android.Content.PM;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using XamarinCRM;
using Syncfusion.SfChart.XForms.Droid;
using ImageCircle.Forms.Plugin.Droid;

namespace XamarinCRMAndroid
{
    [Activity(Label = "XamarinCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);

            FormsMaps.Init(this, bundle);
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.
            ImageCircleRenderer.Init();
            //#if DEBUG
            //Insights.Initialize(Insights.DebugModeKey, this);
            //#else
            Insights.Initialize("2b82ddc37582e6c1bece7e5901e8bae3bf7bfb26", this);
            //#endif

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}


