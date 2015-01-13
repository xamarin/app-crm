using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace MobileCRM.Test.Android
{
    [TestFixture()]
    [Category("TestAndroid")]
    public class AndroidTestSuite
    {
        private AndroidApp app = null;

        [SetUp]
        public void Setup()
        {
            app = ConfigureApp.Android
                .ApkFile(@"../../../MobileCRM.Android/bin/Debug/com.xamarin.Meetum.apk")
                .ApiKey("e2756ca2130df973d555d7a5efe51d43")
                .StartApp();
        }

        [Test]
        public void AVerifyAccountInovice()
        {
            Login();

            app.WaitForThenTap(x => x.Id("up"), "Then I tap the menu button on homescreen");
            app.WaitForThenTap(x => x.Text("Accounts"), "Then I tap the Accounts button");
            app.WaitForThenTap(x => x.Class("android.widget.TextView").Index(0), "Then I tap the first account");
            app.WaitForThenTap(x => x.Text("History"), "Then I tap the history button");
            app.WaitForThenTap(x => x.Text("Paper"), "Then I tap the first order button");
            app.WaitForThenGetTextOfIndexAndCompare(x => x.Class("FormsTextView"), 2, "Paper", "Then I verify that the Paper entry is valid");
        }

        [Test]
        public void BVerifyLeadData()
        {
            Login();

            app.WaitForThenTap(x => x.Id("up"), "Then I tap the menu button on homescreen");
            app.WaitForThenTap(x => x.Text("Leads"), "Then I press the Leads button");
            app.WaitForThenTap(x => x.Class("TextView").Index(2), "Then I tap the second lead");
            app.WaitForThenTap(x => x.Class("android.widget.EditText").Index(1), "Then I select the Industry box");          
            app.IsItThere(x => x.Id("buttonPanel"), "Then I should see the button panel");
            app.WaitForThenTap(x => x.Id("button1"), "Then I close the number picker box");
        }

        [Test]
        public void CVerifyProducts()
        {
            Login();

            app.WaitForThenTap(x => x.Id("up"), "Then I tap the menu button on homescreen");
            app.WaitForThenTap(x => x.Text("Product Catalog"), "Then I press the Product Catalog button");
            //app.IsItThere()
            //app.Repl();
            //app.WaitForThenTap(x => x.Class("TextView").Index(2), "Then I tap the second contact");
            //app.IsItThere(x => x.Class("xamarin.forms.platform.android.EntryCellEditText").Index(0));
            

            //app.WaitForThenTap(x => x.Class("android.widget.EditText").Index(1), "Then I select the Industry box");
            //app.IsItThere(x => x.Id("buttonPanel"), "Then I should see the button panel");
            //app.WaitForThenTap(x => x.Id("button1"), "Then I close the number picker box");
        }


        public void Login()
        {
            if (app.IsItThere(x => x.Text("Login")) == true)
            {
                app.Screenshot("Given when I start the app");

                app.WaitForThenTap(x => x.Text("Login"), "Then I press the Login button");
                app.WaitForThenEnterText(
                    x => x.Id("NoResourceEntry-42").Raw("webView css:'#cred_userid_inputtext'"),
                    "sally@xamcrm.onmicrosoft.com", "Then I enter the user email");
                app.WaitForThenTap(
                    x => x.Id("NoResourceEntry-42").Raw("webView css:'#keep_me_signed_in_label_text'"),
                    "Then I press the Keep Me Signed In checkbox");
                app.WaitForThenEnterText(
                    x => x.Id("NoResourceEntry-42").Raw("webView css:'#cred_password_inputtext'"), "",
                    "Then I enter the user password");
                app.WaitForThenEnterText(
                    x => x.Id("NoResourceEntry-42").Raw("webView css:'#cred_password_inputtext'"), "P@ssword",
                    "Then I enter the user password");
                app.WaitForThenTap(x => x.Id("NoResourceEntry-42").Raw("webView css:'#cred_sign_in_button'"),
                    "Then I press the sign-in button");
            }
            else
            {
                app.Screenshot("Given when I start the app");
            }

            Thread.Sleep(2000);
            app.SetOrientationLandscape();
            app.Screenshot("Then I set the orientation landscape");
            Thread.Sleep(2000);
            app.SetOrientationPortrait();
            app.Screenshot("Then I set the orientation portrait");
        }

        //public void CompareText(string sExpected, string sActual)
        //{
        //    if (string.IsNullOrEmpty(sActual) == false && string.IsNullOrEmpty(sExpected) == false)
        //    {
        //        if (String.Compare(sExpected, sActual) != 0) //not equal
        //        {
        //            Assert.IsTrue(false, string.Format("TEST FAILURE. Expecting: {0} , Actual: {1}",sExpected, sActual));
        //        }

        //    }
        //}


    }

}
