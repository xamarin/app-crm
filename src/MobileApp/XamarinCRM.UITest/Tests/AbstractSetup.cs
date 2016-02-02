using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class AbstractSetup
    {
        protected IApp app;
        protected Platform platform;

        protected AbstractSetup(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            if (app.Query("SIGN IN").Any())
            {
                new SplashScreenPage(app, platform)
                        .ExitSplashScreen();
            }

            //waiting for next screen to load
            Thread.Sleep(TimeSpan.FromSeconds(5));

            if (app.Query(x => x.WebView()).Any())
            {
                LogIn();
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));
            app.WaitForNoElement("Loading sales data...");
            //Refreshing the data on home page
//            new GlobalPage(app, platform)
//                .navigateToProducts();
//
//            new GlobalPage(app, platform)
//                .navigateToSales();
//
            app.Screenshot("On Home Page");
        }

        [TearDown]
        public virtual void AfterEachTest()
        {
            if (platform == Platform.iOS)
                // wipe the device keychain
                app.TestServer.Post("/keychain");
        }
            
        void LogIn()
        {
            var deviceIndex = Environment.GetEnvironmentVariable("XTC_DEVICE_INDEX");
            var accounts = new List<string[]>();

            using (var accountsStream = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("XamarinCRM.UITest.accounts.csv")))
            {
                string line;
                while ((line = accountsStream.ReadLine()) != null)
                    accounts.Add(line.Split(','));
            }

            int userNumber = (deviceIndex == null) ? 0 : Int32.Parse(deviceIndex) % accounts.Count;
            if (userNumber >= accounts.Count)
                throw new IndexOutOfRangeException($"Only enough logins for {accounts.Count} users, {userNumber} is out of range.");

            var user = accounts[userNumber][0];
            var password = accounts[userNumber][1];
            
            new LoginPage(app, platform)
                .LogInWithWorkCredentials(user, password);

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
