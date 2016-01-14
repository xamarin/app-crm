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
        readonly Query SaveButton;

        readonly Query RoleField;
        readonly Query FirstNameField;
        readonly Query LastNameField;
        readonly Query PhoneField;
        readonly Query EmailField;
        readonly Query AddressField;
        readonly Query PostalCodeField;
        readonly Query CityField;
        readonly Query StateField;
        readonly Query CountryField;

        public LeadDetailsPage(IApp app, Platform platform)
            : base(app, platform, "Opportunity", "OPPORTUNITY")
        {
            if (OnAndroid)
            {
                ContactTab = x => x.Marked("Contact");
                EditCompany = x => x.Marked("Company").Sibling();
                EditIndustry = x => x.Class("android.widget.EditText").Index(1);
                EditSize = x => x.Marked("Size").Sibling();
                EditStage = x => x.Class("android.widget.EditText").Index(3);
                Done = x => x.Marked("Save");
                Save = x => x.Marked("Save");
                ScrollPanel = x => x.Id("contentPanel");
                OkButton = x => x.Id("button2");
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

                RoleField = x => x.Marked("Role").Sibling();
                FirstNameField = x => x.Marked("First Name").Sibling();
                LastNameField = x => x.Marked("Last Name").Sibling();
                PhoneField = x => x.Marked("Phone").Sibling();
                EmailField = x => x.Marked("Email").Sibling();
                AddressField = x => x.Marked("Address").Sibling();
                PostalCodeField = x => x.Marked("Postal Code").Sibling();
                CityField = x => x.Marked("City").Sibling();
                StateField = x => x.Marked("State").Sibling();
                CountryField = x => x.Marked("Country").Sibling();
                SaveButton = x => x.Id("save.png");
            }
        }

        public void GoToLeadContact()
        {
            if (OnAndroid)
            {
                app.ScrollDownTo("Phone");
            }
            if (OniOS)
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
            if (OniOS)
                app.Tap(SaveButton);
            if (OnAndroid)
                app.Tap(Save);

            app.Screenshot("Save dialog appears");
            app.Tap(Save);
        }

        public LeadDetailsPage SelectNewIndustry(string industry)
        {
            if (OnAndroid)
            {
                app.Tap(EditIndustry);
                app.ScrollDown(ScrollPanel);
                try{
                    app.Tap(industry);
                }
                catch {
                    app.Tap(OkButton);
                }
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
                try {
                    app.Tap(stage);
                }
                catch {
                    app.Tap(OkButton);
                }
            }
            if (OniOS)
            {
                app.Tap(EditStage);
                app.Tap(stage);
                app.Tap(DismissSelection);
            }

            return this;
        }

        public LeadDetailsPage EnterLeadContact(
            string role,
            string firstName,
            string lastName,
            string phone,
            string email,
            string address,
            string postalCode,
            string city,
            string state,
            string country
        )
        {
            if (OniOS)
            {
                app.Tap(RoleField);
                app.EnterText(role);
                app.PressEnter();

                app.EnterText(FirstNameField, firstName);
                app.PressEnter();

                app.EnterText(LastNameField, lastName);
                app.PressEnter();

                app.EnterText(PhoneField, phone);
                app.DismissKeyboard();

                app.EnterText(EmailField, email);
                app.PressEnter();

                app.ScrollDownTo("Address");
                app.EnterText(AddressField, address);
                app.PressEnter();

                app.ScrollDownTo("Postal Code");
                app.EnterText(PostalCodeField, postalCode);
                app.DismissKeyboard();

                app.ScrollDownTo("City");
                app.EnterText(CityField, city);
                app.PressEnter();

                app.ScrollDownTo("State");
                app.EnterText(StateField, state);
                app.PressEnter();

                app.ScrollDownTo("Country");
                app.EnterText(CountryField, country);
                app.PressEnter();
            }
            return this;
        }
    }
}

