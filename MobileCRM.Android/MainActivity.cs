using Android.App;
using Android.OS;
using Xamarin.Forms;
using Xamarin;
using Android.Content.PM;

namespace MobileCRMAndroid
{
    [Activity(Label = "VervetaCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c", this);

            // Set our view from the "main" layout resource
            SetPage(MobileCRM.Shared.App.RootPage);
        }
    }
}


