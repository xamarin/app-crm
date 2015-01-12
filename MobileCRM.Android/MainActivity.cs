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

            Insights.Initialize("be25cc3485829d5bca63e3ba54797cdd9365186c", this);

            // Set our view from the "main" layout resource
            SetPage (MobileCRM.Shared.App.RootPage);
        }

        
    }
}


