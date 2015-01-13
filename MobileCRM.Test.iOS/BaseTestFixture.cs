#define iOSSimulator

using System;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using NUnit.Framework;


namespace MobileCRM.Test
{
    public class BaseTestFixture
    {
        protected static iOSApp app;
        protected static int longTimeout = 180;
        protected static int shortTimeout = 30;

        [SetUp]
        protected void Setup ()
        {
#if iOSSimulator
            app = ConfigureApp
                .iOS
				.AppBundle("MobileCRM.app")
				.DeviceIdentifier("iPhone 5 (8.0 Simulator)")
                .StartApp ();
#else
			app = ConfigureApp
				.iOS
				.InstalledApp ("com.stevenyi.mobilecrm")
				.StartApp ();
#endif

            WaitForLoad ();
        }

        private void WaitForLoad ()
        {
            // Wait for graph
            app.WaitForNoElement(
                query: PlatformQueries.LoadingIcon,
                timeoutMessage: "Timed out waiting for graph to load",
                timeout: TimeSpan.FromSeconds(longTimeout),
                postTimeout: TimeSpan.FromSeconds(2)
            );
            app.Screenshot("Screenshot of the dashboard");

            //tap on the menu button
            app.Tap (PlatformQueries.SlideoutMenu);
            app.WaitForElement (
                query: x => x.Text ("Accounts"),
                timeout: TimeSpan.FromSeconds(longTimeout),
                postTimeout: TimeSpan.FromSeconds(2)
            );
            app.Screenshot ("Tapped the Menu button");
        }
    }
}

