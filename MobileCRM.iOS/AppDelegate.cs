using Microsoft.WindowsAzure.MobileServices;
using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace MobileCRM.iOS
{
//        [Register("AppDelegate")]
//        public partial class AppDelegate : FormsApplicationDelegate
//        {
//            UIWindow window;
//    
//            public override bool FinishedLaunching(UIApplication app, NSDictionary options)
//            {
//    #if DEBUG
//                //Xamarin.Calabash.Start();
//    #endif
//    
//                CurrentPlatform.Init();
//                SQLitePCL.CurrentPlatform.Init();
//                window = new UIWindow(UIScreen.MainScreen.Bounds);
//    
//                Forms.Init();
//                FormsMaps.Init();
//    
//                LoadApplication (new App ());
//    
//                return base.FinishedLaunching (app, options);
//    
//                window.RootViewController = App.RootPage.CreateViewController();
//                window.MakeKeyAndVisible();
//    
//                return true;
//            }
//        }

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            #if DEBUG
            //Xamarin.Calabash.Start();
            #endif

            // Azure Mobile Services initilization
            CurrentPlatform.Init();

            // SQLite initilization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initilization
            global::Xamarin.Forms.Forms.Init();

            // Bootstrap the PCL app
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}