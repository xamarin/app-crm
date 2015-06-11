using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin;

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
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            Forms.Init();
            FormsMaps.Init();

            window.RootViewController = MobileCRM.App.RootPage.CreateViewController();
            window.MakeKeyAndVisible();
            
            return true;
        }
    }
}