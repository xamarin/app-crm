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

            new SfChartRenderer();

            Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c", this);

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


