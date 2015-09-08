using System;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class SalesHomePage : BasePage
    {

        readonly Query FirstLead;
        readonly Query ListView;
        readonly Query AddLeadButton;

        public SalesHomePage(IApp app, Platform platform)
            : base(app, platform, "WEEKLY AVERAGE", "WEEKLY AVERAGE")
        {
            if (OnAndroid)
            {
                FirstLead = x => x.Marked("50% - Value Proposition");
                ListView = x => x.Id("content");
                AddLeadButton = x => x.Class("FormsImageView");
            }
            if (OniOS)
            {
                FirstLead = x => x.Class("UITableViewCellContentView");
                ListView = x => x.Class("UILayoutContainerView");
                AddLeadButton = x => x.Id("add_ios_gray");
            }
        }

        public void ClickOnFirstLead()
        {
            app.Tap(FirstLead);
        }

        public SalesHomePage VerifyLeadPresent(
            string company,
            string opportunity,
            string size,
            string stage)
        {
            try
            {
                app.ScrollDownTo(company, timeout: TimeSpan.FromSeconds(15));
            }
            catch
            {
                throw new Exception("Could not locate lead with company name: " + company);
            }

            app.WaitForElement("$" + size, timeoutMessage: "Correct size of lead did not appear");
            app.Screenshot("Sales lead is updated");

            return this;
        }

        public SalesHomePage RefreshLeads()
        {
            var list = app.Query(ListView)[0].Rect;
            float startY = (float)(list.Y * 0.50);
            float endY = (float)(list.Y * 0.10);
            //   app.DragCoordinates(list.CenterX, list.Y * 0.7, list.CenterX, list.Y * 0.2 + list.Height);
            app.DragCoordinates(list.CenterX, startY, list.CenterX, endY);

            return this;
        }

        public void AddNewLead()
        {
            app.Tap(AddLeadButton);
        }
    }
}

