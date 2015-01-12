using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin;
using MobileCRM.Shared.Pages;
using MobileCRM;
using Android.Graphics.Drawables;
using Android.Content.PM;
using MobileCRM.Shared;

namespace MobileCRMAndroid
{
    [Activity (Label = "MobileCRM", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.AndroidActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            Insights.Initialize("2280b2feeb6ba19b86550b7b68969fe0af96cc87", this);

            // Set our view from the "main" layout resource
            SetPage (MobileCRM.Shared.App.RootPage);
        }

        
    }
}


