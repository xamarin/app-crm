using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using NUnit.Framework;

namespace MobileCRM.Test
{
    public class BaseTestFixture
    {
        protected static AndroidApp app;
		private static readonly string apkPath = "../../com.xamarin.Meetum-Signed.apk";
        private static readonly string keystorePath = @"debug.keystore";
        protected static int longTimeout = 180;
        protected static int shortTimeout = 30;

        [SetUp]
        protected void Setup ()
        {
            app = ConfigureApp
                .Android
                .KeyStore (keystorePath)
                .ApkFile (apkPath)
                .StartApp ();

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
            app.Screenshot("Screen shot of the dashboad.");

            //tap on the menu button
            app.Tap (PlatformQueries.SlideoutMenu);
            app.WaitForElement (
                query: x => x.Text ("Accounts"),
                timeout: TimeSpan.FromSeconds(longTimeout),
                postTimeout: TimeSpan.FromSeconds(2)
            );
            app.Screenshot ("Tapped on the Menu button");
        }
    }
}

