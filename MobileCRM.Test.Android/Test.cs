using NUnit.Framework;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using System.Threading;
using System.Linq;

namespace MobileCRM.Test
{
    [TestFixture]
    public class Test : BaseTestFixture
    {

		[Test]
		public void AccountReview ()
		{
			//Tap on an accounts page
			app.Tap(e=>e.Text("Accounts"));

			app.WaitForElement (
				query: e => e.Text ("Joan Mancum"),
				timeout: TimeSpan.FromSeconds(longTimeout)
			);
			app.Screenshot("Accounts screen");

			//Tap on an account
			app.Tap (e => e.Text ("Joan Mancum"));
			app.WaitForNoElement (
				query: PlatformQueries.LoadingIcon,
				timeoutMessage: "Timed out waiting for graph to load",
				timeout: TimeSpan.FromSeconds (longTimeout),
				postTimeout: TimeSpan.FromSeconds (2)
			);
			app.Screenshot("Bay Unified Scool District Account Page");

			//View history of an account
			app.Tap (x => x.Text ("History"));
			app.WaitForElement (e => e.Text ("Ink"));
			app.Screenshot ("View the purchasing history of Bay Unified School Disctrict");

			//Tap on an order
			app.Tap (x => x.Text ("Ink").Index (0));
			app.WaitForElement (
				query: e => e.Text ("Signature:"),
				postTimeout: TimeSpan.FromSeconds(2)
			);
			app.Screenshot ("View an Order Invoice for Bay Unified School District");

		}

		[Test]
		public void AddNewLead ()
		{
			//We should be viewing the menu list
			app.Tap (x => x.Text ("Leads"));
			app.WaitForElement (
				query: e => e.Text ("Acme Co."),
				timeout: TimeSpan.FromSeconds(longTimeout)
			);

			app.Screenshot("We are currently viewing our Leads list");

			//Tap on the plus button to add a new lead
			app.Tap (x => x.Marked ("add"));
			app.WaitForElement (e => e.Text ("Map"));
			app.Screenshot("Adding a new lead will bring us to the New Lead screen");

			//Adding text to the Company Tab
			app.WaitForElement (PlatformQueries.Entry);
			app.EnterText (PlatformQueries.EntryWithIndex(0), "XYZ Co.");

			app.Tap (x => x.Text ("None Selected").Index(0));
			app.WaitForElement (PlatformQueries.Picker);
			app.Screenshot("After selecting the Industry, a pop up window will appear.");

			//Select the industry
			var buffer = 10;
			var viewBounds = app.Query (PlatformQueries.Picker) [0].Rect;
			var x1 = viewBounds.CenterX;
			var y1 = (float)(viewBounds.Y + (0.7 * viewBounds.Height));
			var y2 = viewBounds.Y + buffer;

			app.Drag (x1, y1, x1, y2);

			Thread.Sleep (1000);
			if (app.Query (x => x.Text ("OK")).Any ())
				app.Tap (x => x.Text ("OK"));
			else
				app.Tap (x => x.Text ("Done").Index (1));

			app.Query (x => x.Text ("0").Invoke ("setText", "500"));

			app.Tap (x => x.Text ("None Selected").Index (0));
			app.WaitForElement (PlatformQueries.Picker);
			app.Screenshot("After selecting the Opportunity Stage, a pop up window will appear.");

			app.Drag (x1, y1, x1, y2);

			Thread.Sleep (1000);
			// Had to do this hacky way of things because there were two 
			// Buttons with text "Done" and I didn't want to create another
			// version of PickerConfirm in PlatformQueries.  If you would like
			// to make this more elegant, just put the following in PlatformQueries
			// ios:  public static Func<AppQuery, AppQuery> OtherPickerConfirm = x => x.Text("Done").Index(1);
			// android: public static Func<AppQuery, AppQuery> OtherPickerConfirm = x => x.Text("OK");
			// Then put: 
			// app.Tap(PlatformQueries.OtherPickerConfirm); here
			// ... I should have just done that...
			if (app.Query (x => x.Text ("OK")).Any ())
				app.Tap (x => x.Text ("OK"));
			else
				app.Tap (x => x.Text ("Done").Index (1));

			app.Screenshot("All 'Company' fields have been filled out.");

			app.Tap (x => x.Text ("Contact"));

			app.WaitForElement (
				query: PlatformQueries.EntryCell,
				timeout: TimeSpan.FromSeconds (shortTimeout),
				timeoutMessage: "Timed out waiting for contact screen",
				postTimeout: TimeSpan.FromSeconds (2)
			);

			app.Tap (PlatformQueries.EntryCellWithIndex(0));
			app.EnterText ("394 Pacific Ave");
			Thread.Sleep (500);

			app.Tap (PlatformQueries.EntryCellWithIndex(1));
			app.EnterText ("San Francisco");
			Thread.Sleep (500);

			app.Tap (PlatformQueries.EntryCellWithIndex(2));
			app.EnterText ("CA");
			Thread.Sleep (500);

			app.Tap (PlatformQueries.EntryCellWithIndex(3));
			app.EnterText ("94133");
			Thread.Sleep (500);

			app.Tap (PlatformQueries.EntryCellWithIndex(4));
			app.EnterText ("USA");
			Thread.Sleep (1000);

			if (app is Xamarin.UITest.iOS.iOSApp) {
				app.DismissKeyboard ();
			}

			app.Screenshot ("All 'Contact' fields have been filled out");
			app.Tap (x => x.Text ("Map"));
			app.WaitForElement (
				query: PlatformQueries.MapView,
				postTimeout: TimeSpan.FromSeconds(20),
				timeout: TimeSpan.FromSeconds(shortTimeout),
				timeoutMessage: "Timed out waiting for map"
			);

			app.Screenshot ("Viewing map page");

			app.Tap (e => e.Text ("Done"));
			app.WaitForElement (e => e.Text ("Save"));
			app.Screenshot("Confirm addition of the new lead");
			app.Tap (e => e.Text ("Save"));
			app.WaitForElement (
				query: e => e.Text ("Acme Co."),
				timeout: TimeSpan.FromSeconds(shortTimeout),
				postTimeout: TimeSpan.FromSeconds(2),
				timeoutMessage: "Timed out waiting to return to leads page"
			);
			app.Screenshot("Viewing the leads page again.");

		}


        [Test]
        public void MapsScreenShot ()
        {
            //Tap on an accounts page
            app.Tap (e => e.Text ("Accounts"));
            app.WaitForElement (
                query: e => e.Text ("Joan Mancum"),
                timeout: TimeSpan.FromSeconds(longTimeout)
            );
            app.Screenshot("Accounts screen");

            //Tap on an account
            app.Tap (e => e.Text ("Joan Mancum"));
            app.WaitForNoElement (PlatformQueries.LoadingIcon,
                timeoutMessage: "Timed out waiting for graph to load",
                timeout: TimeSpan.FromSeconds (longTimeout),
                postTimeout: TimeSpan.FromSeconds (2)
            );
            app.Screenshot ("Company information");


            //Tap Mapsmono /Users/abarlow/Projects/Rdio.UITest/packages/Xamarin.UITest.0.5.0/tools/test-cloud.exe submit /Users/abarlow/Projects/Rdio.UITest/Rdio.UITest.Android/bin/Debug/rdio.apk edfd3124c92123c8a11e2379c84ee36c --devices 78ef44bd --series "master" --locale "en_US" --app-name "Rdio" --assembly-dir /Users/abarlow/Projects/Rdio.UITest/Rdio.UITest.Android/bin/Debug/
            app.Tap (e => e.Text ("Map"));
            app.WaitForElement (
                query: PlatformQueries.MapView,
                timeoutMessage: "Timed out waiting for map to load",
                timeout: TimeSpan.FromSeconds(shortTimeout),
                postTimeout: TimeSpan.FromSeconds(10)
            );
            app.Screenshot ("Map of Joan Mancum");

            /*We need to tap the pin and show the address of the account on the map.*/
            // If I end up with time, I'll come up with a hack to tap the pin.  It isn't an item represented on the screen... -Austin

        }



        [Test]
        public void AddNewOrder ()
        {

            //Tap on an accounts page
            app.Tap(e=>e.Text("Accounts"));
            app.WaitForElement (
                query: e => e.Text ("Joan Mancum"),
                timeout: TimeSpan.FromSeconds(longTimeout)
            );
            app.Screenshot("Accounts screen");

            //Tap on an account
            app.Tap (e => e.Text ("Joan Mancum"));
            app.WaitForNoElement (PlatformQueries.LoadingIcon,
                timeoutMessage: "Timed out waiting for graph to load",
                timeout: TimeSpan.FromSeconds (longTimeout),
                postTimeout: TimeSpan.FromSeconds (2)
            );
            app.WaitForElement (e => e.Text ("COMPANY INFO"));
            //app.WaitForNoElement (e => e.Class ("android.widget.ProgressBar"));
            app.Screenshot("Bay Unified Scool District Account Page");

            app.Tap (e => e.Text ("Orders"));
            app.WaitForElement (e =>e.Text ("New Order"));
            app.Screenshot("Screenshot of the Orders page");

            app.Tap (e => e.Text ("New Order"));
            app.WaitForElement (e => e.Text("Place Order"));
            app.Screenshot("What placing an order looks like");

            app.Tap (e => e.Text ("Paper"));
            app.WaitForElement (PlatformQueries.Picker);
            app.Screenshot("Select a Product");

            var buffer = 10;
            var viewBounds = app.Query (PlatformQueries.Picker) [0].Rect;
            var x1 = viewBounds.CenterX;
            var y1 = (float)(viewBounds.Y + (0.7 * viewBounds.Height));
            var y2 = viewBounds.Y + buffer;
            app.Drag (x1, y1, x1, y2);

            app.Repl ();
            app.Tap (PlatformQueries.PickerConfirm);
            app.WaitForNoElement (PlatformQueries.Picker);
            app.Screenshot("Product has been selected");
            var date = app.Query (PlatformQueries.TextFieldLabel).Last ().Text;

            //Select the Price
            var price = new Random ().Next (99999).ToString ();
            app.EnterText (x => x.Text ("0"), price);
            app.Screenshot("Price has been added the the order");
            //Place your order
            // Note that this double wait is necessary on iOS because
            // The items pop up for a moment,  disappear, then reappear
            app.Tap (e => e.Text ("Place Order"));
            Thread.Sleep (2000);
            app.WaitForElement (
                query:x => x.Text ("Paper"),
                timeout: TimeSpan.FromSeconds(longTimeout)
            );
            Thread.Sleep (2000);
            app.WaitForElement (
                query:x => x.Text ("Paper"),
                timeout: TimeSpan.FromSeconds(longTimeout)
            );
            int numScrolls = 0;
            viewBounds = app.Query (PlatformQueries.List).First().Rect;
            x1 = viewBounds.CenterX;
            y1 = (float)(viewBounds.Y + (0.8 * viewBounds.Height));
            y2 = (float)(viewBounds.Y + (0.2 * viewBounds.Height));
            while (!app.Query (x => x.Text (String.Format ("Price Quote: ${0} | Due: {1}", price, date))).Any()) {
                app.Drag (x1, y1, x1, y2);
                numScrolls++;
                if (numScrolls == 100)
                    break;
            }
            app.WaitForElement (x => x.Text (String.Format ("Price Quote: ${0} | Due: {1}", price, date)));
            app.Screenshot("Order has been placed");
        }

        [Test]
        public void OrderPageSigniture ()
        {
            app.Tap(e=>e.Text("Accounts"));
            app.WaitForElement (
                query: e => e.Text ("Joan Mancum"),
                timeout: TimeSpan.FromSeconds(longTimeout)
            );
            app.Screenshot("Accounts screen");

            app.Tap (e => e.Text ("Joan Mancum"));

            app.WaitForNoElement (PlatformQueries.LoadingIcon,
                timeoutMessage: "Timed out waiting for graph to load",
                timeout: TimeSpan.FromSeconds (longTimeout),
                postTimeout: TimeSpan.FromSeconds (2)
            );
            app.Screenshot("Bay Unified Scool District Account Page");

            app.Tap (x => x.Text ("History"));
            app.WaitForElement (e => e.Text ("Ink"));
            app.Screenshot ("View the purchasing history of Bay Unified School Disctrict");

            app.Tap (x => x.Text ("Ink"));
            app.WaitForElement (PlatformQueries.Signature);
            Thread.Sleep(2000);
            app.Screenshot ("View the Signature in Portrate mode");

            app.SetOrientationLandscape ();
            Thread.Sleep (2000);
            app.Screenshot("Viewing the Signature in Lanscape mode.");
        }

        [Test]
        public void ProductCatalog()
        {

            app.Tap (e => e.Text ("Product Catalog"));
            app.WaitForElement (e => e.Text ("Paper"));
            app.Screenshot ("Product Catalog");
        }
    }
}



