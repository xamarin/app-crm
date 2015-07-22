using Microsoft.WindowsAzure.MobileServices;
using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;

namespace MobileCRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if DEBUG
            //Xamarin.Calabash.Start();
#endif

            CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            Forms.Init();
            FormsMaps.Init();

            window.RootViewController = App.RootPage.CreateViewController();
            window.MakeKeyAndVisible();
            
            return true;
        }
    }
}