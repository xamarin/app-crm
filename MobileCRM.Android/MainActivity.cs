using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin;
using Android.Content.PM;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using MobileCRM;
using Syncfusion.SfChart.XForms.Droid;

namespace MobileCRMAndroid
{
    [Activity(Label = "MobileCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);

            FormsMaps.Init(this, bundle);

            new SfChartRenderer();

            Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c", this);

            Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Never);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}


