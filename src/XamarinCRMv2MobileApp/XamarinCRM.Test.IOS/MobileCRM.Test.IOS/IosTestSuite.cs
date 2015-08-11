using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using System.Threading;
using Xamarin.UITest.iOS;

namespace XamarinCRM
{
    [TestFixture()]
    [Category("TestiOS")]
    public class IosTestSuite
    {
        private iOSApp app = null;

		[SetUp]
		[Category("TestiOS")]
        public void Setup()
        {
			app = ConfigureApp.iOS
			//.AppBundle(@"/Users/glennwester/Projects/mobilecrm-salesGLENN/MobileCRM.iOS/bin/iPhoneSimulator/Debug/MobileCRM.app")
				.InstalledApp ("com.glennwester.mobilecrm")
				.ApiKey ("88fdd82057296d11865e383707313d29")
                .StartApp();
        }

        [Test]
        public void AiOSVerifyAccountInovice()
        {
            Login();

			app.WaitForThenTap(x => x.Id("slideout.png"), "Then I tap the menu button on homescreen");
			app.WaitForThenTap(x => x.Id("account.png"), "Then I tap the Accounts button");
			app.WaitForThenTap(x => x.Text("Bay Unified School District"), "Then I tap the first account");
			app.WaitForThenTap (x => x.Id ("orderhistory.png"), "Then I tap the order history button");
			app.WaitForThenGetTextOfIndexAndCompare(x => x.Text("Paper"), 0, "Paper", "Then I verify that the Paper entry is valid");
        }

        public void Login() 
        {
			app.Screenshot("Given when I start the app");
			app.WaitForThenTap(x => x.Text("Login"), "Then I press the Login button");
			app.EnterText (x => x.Class ("UIWebView").Index (0).Raw ("webView css:'#cred_password_inputtext'"), "sally@xamcrm.onmicrosoft.com");
			app.EnterText(x => x.Class("UIWebView").Index(0).Raw("webView css:'#keep_me_signed_in_label_text'"), "P@ssword");
			app.Tap(x => x.Class("UIWebView").Index(0).Raw("webView css:'#cred_forgot_password_link'"));

            Thread.Sleep(2000);
            app.SetOrientationLandscape();
            app.Screenshot("Then I set the orientation landscape");
            Thread.Sleep(2000);
            app.SetOrientationPortrait();
            app.Screenshot("Then I set the orientation portrait");
        }
			
    }
}
