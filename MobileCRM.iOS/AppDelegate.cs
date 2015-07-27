using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
<<<<<<< Updated upstream
using Xamarin;
=======
using Xamarin.Forms.Platform.iOS;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
#endif
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
=======
            #endif

            // Azure Mobile Services initilization
            CurrentPlatform.Init();

            // SQLite initilization
>>>>>>> Stashed changes
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initilization
            global::Xamarin.Forms.Forms.Init();

            // Bootstrap the PCL app
            LoadApplication(new App());

<<<<<<< Updated upstream
            window.RootViewController = MobileCRM.App.RootPage.CreateViewController();
            window.MakeKeyAndVisible();
            
            return true;
=======
            return base.FinishedLaunching(app, options);
>>>>>>> Stashed changes
        }
    }
}