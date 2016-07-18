using Foundation;
using HockeyApp.iOS;
using ImageCircle.Forms.Plugin.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinCRM.Pages;
using XamarinCRM.Statics;

namespace XamarinCRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
       	{
			var manager = BITHockeyManager.SharedHockeyManager;
			// Set the HockeyApp App Id here:
			manager.Configure("11111111222222223333333344444444"); // This is just a dummy value. Replace with your real HockeyApp App ID
			manager.StartManager();

            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.

            #if DEBUG
            Xamarin.Calabash.Start();
            ObjCRuntime.Dlfcn.dlopen("recorderPluginCalabash.dylib", 0);
            #endif

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // SQLite initialization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initialization
            Forms.Init();

            // Xamarin.Forms.Maps initialization
            Xamarin.FormsMaps.Init();

			ImageCircleRenderer.Init ();

            // Bootstrap the "core" Xamarin.Forms app
            LoadApplication(new App());

			// Apply OS-specific color theming
			ConfigureApplicationTheming ();

            return base.FinishedLaunching(app, options);
        }

		void ConfigureApplicationTheming ()
		{
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = Palette._001.ToUIColor ();
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = UIColor.White }, UIControlState.Normal);

			UITabBar.Appearance.TintColor = UIColor.White;
			UITabBar.Appearance.BarTintColor = UIColor.White;
			UITabBar.Appearance.SelectedImageTintColor = Palette._003.ToUIColor ();
			UITabBarItem.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = Palette._003.ToUIColor () }, UIControlState.Selected);

			UIProgressView.Appearance.ProgressTintColor = Palette._003.ToUIColor ();
		}

        public const string First = "com.xamarin.xamarincrm.001";
        public const string Second = "com.xamarin.xamarincrm.002";
        public const string Third = "com.xamarin.xamarincrm.003";

        public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
        public override void OnActivated (UIApplication application)
        {
            Console.WriteLine ("ccccccc OnActivated");

            // Handle any shortcut item being selected
            HandleShortcutItem(LaunchedShortcutItem);

            // Clear shortcut after it's been handled
            LaunchedShortcutItem = null;
        }

        // if app is already running
        public override void PerformActionForShortcutItem (UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            Console.WriteLine ("dddddddd PerformActionForShortcutItem");
            // Perform action
            var handled = HandleShortcutItem(shortcutItem);
            completionHandler(handled);
        }
        public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem) {
            Console.WriteLine ("eeeeeeeeeee HandleShortcutItem ");
            var handled = false;

            // Anything to process?
            if (shortcutItem == null) 
                return false;

            // Take action based on the shortcut type
            switch (shortcutItem.Type) {
                case First:
                    App.GoToRoot();
                    ((RootTabPage)App.Current.MainPage).CurrentPage = ((RootTabPage)App.Current.MainPage).Children[0];
                    handled = true;
                    break;
                case Second:
                    App.GoToRoot();
                    ((RootTabPage)App.Current.MainPage).CurrentPage = ((RootTabPage)App.Current.MainPage).Children[1];

                    handled = true;
                    break;

                case Third:
                    App.GoToRoot();
                    ((RootTabPage)App.Current.MainPage).CurrentPage = ((RootTabPage)App.Current.MainPage).Children[2];

                    handled = true;
                    break;
            }

            Console.Write (handled);
            // Return results
            return handled;
        }

     
    }
}