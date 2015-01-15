using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using System.Threading;
using Xamarin.UITest.iOS;

namespace MobileCRM.Test.IOS
{
    [TestFixture()]
    public class AccountVerification
    {
        private iOSApp app = null;

		[SetUp]
        public void Setup()
        {
			app = ConfigureApp.iOS
				//.InstalledApp ("com.mycompany.mobilecrm")
                .StartApp();
        }

        [Test]
        public void VerifyAccountHasPaperItem()
        {
			app.Screenshot("Given when I start the app");

			app.WaitForThenTap(x => x.Id("slideout.png"), "Then I tap the menu button on homescreen");
			app.WaitForThenTap(x => x.Id("account.png"), "Then I tap the Accounts button");
			app.WaitForThenTap(x => x.Text("Bay Unified School District"), "Then I tap the first account");
			app.WaitForThenTap (x => x.Text ("History"), "Then I tap the order history button");
			app.WaitForThenGetTextOfIndexAndCompare(x => x.Text("Paper"), 0, "Paper", "Then I verify that we have a Paper item");
        }
			
			
    }
}
