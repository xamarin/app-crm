using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin;
using Android.Content.PM;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using MobileCRM;

namespace MobileCRMAndroid
{
<<<<<<< Updated upstream
    [Activity(Label = "VervetaCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.AndroidActivity
=======
    [Activity(Label = "MobileCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
>>>>>>> Stashed changes
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c", this);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}


