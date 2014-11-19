using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin;
using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared.Pages;
using MobileCRM.Shared;
using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Services;

namespace MobileCRM.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;


        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {

#if DEBUG
            //Xamarin.Calabash.Start();
#endif

          Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
          SQLitePCL.CurrentPlatform.Init();
            window = new UIWindow (UIScreen.MainScreen.Bounds);

            Forms.Init();
            FormsMaps.Init();

            //UIStatusBarStyle = UIStatusBarStyle.LightContent;

            window.RootViewController = MobileCRM.Shared.App.RootPage.CreateViewController();
            window.MakeKeyAndVisible ();
            
            return true;
        }

      
    }
}

