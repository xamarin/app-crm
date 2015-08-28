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
using Android.Graphics.Drawables;

namespace XamarinCRMAndroid
{
    [Activity(Label = "XamarinCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);

            FormsMaps.Init(this, bundle);

            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.

            Insights.Initialize("2b82ddc37582e6c1bece7e5901e8bae3bf7bfb26", this);

            LoadApplication(new App());

            if ((int)Build.VERSION.SdkInt >= 21) 
            { 
                ActionBar.SetIcon ( new ColorDrawable (Resources.GetColor (Android.Resource.Color.Transparent))); 
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}


