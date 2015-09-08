using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class LeadDetailsPage : BasePage
    {
        readonly Query ContactTab;
        readonly Query EditCompany;
        readonly Query EditIndustry;
        readonly Query EditSize;
        readonly Query EditStage;
        readonly Query Done;
        readonly Query Save;
        readonly Query ScrollPanel;
        readonly Query OkButton;
        readonly Query DismissSelection;

        public LeadDetailsPage(IApp app, Platform platform)
            : base(app, platform, "Opportunity", "OPPORTUNITY")
        {
            if (OnAndroid)
            {
                ContactTab = x => x.Id("action_bar_container").Descendant().Text("Contact");
                EditCompany = x => x.Marked("Company").Sibling();
                EditIndustry = x => x.Class("android.widget.EditText").Index(1);
                EditSize = x => x.Marked("Size").Sibling();
                EditStage = x => x.Class("android.widget.EditText").Index(3);
                Done = x => x.Marked("Save");
                Save = x => x.Marked("Save");
                ScrollPanel = x => x.Marked("customPanel");
                OkButton = x => x.Marked("button1");
            }
            if (OniOS)
            {
                ContactTab = x => x.Class("UITabBarButtonLabel").Text("Contact");
                EditCompany = x => x.Class("UITextFieldLabel").Index(0);
                EditIndustry = x => x.Class("UITextFieldLabel").Index(1);
                EditSize = x => x.Marked("Size").Sibling();
                EditStage = x => x.Class("UITextFieldLabel").Index(3);
                Done = x => x.Marked("DONE");
                Save = x => x.Marked("Save");
                DismissSelection = x => x.Marked("Done");
            }
        }

        public void GoToLeadContact()
        {
            app.Tap(ContactTab);
        }

        public LeadDetailsPage EnterLeadDetails(
            string company,
            string industry,
            string size,
            string stage)
        {
            app.Tap(EditCompany);
            app.ClearText();
            app.ClearText();
            app.EnterText(company);
            app.DismissKeyboard();

            SelectNewIndustry(industry);

            app.ClearText(EditSize);
            app.EnterText(EditSize, size);
            app.DismissKeyboard();

            SelectNewStage(stage);

            return this;
        }

        public void SaveLead()
        {
            app.Tap(Save);
            app.Tap(Save);
        }

        public LeadDetailsPage SelectNewIndustry(string industry)
        {
            if (OnAndroid)
            {
                app.Tap(EditIndustry);
                app.ScrollDown(ScrollPanel);
                app.Tap(OkButton);
            }
            if (OniOS)
            {
                app.Tap(EditIndustry);
                app.Tap(industry);
                app.Tap(DismissSelection);
            }

            return this;
        }

        public LeadDetailsPage SelectNewStage(string stage)
        {
            if (OnAndroid)
            {
                app.Tap(EditStage);
                app.ScrollDown(ScrollPanel);
                app.Tap(OkButton);
            }
            if (OniOS)
            {
                app.Tap(EditStage);
                app.Tap(stage);
                app.Tap(DismissSelection);
            }

            return this;
        }
    }
}

