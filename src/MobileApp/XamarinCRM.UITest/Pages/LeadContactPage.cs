using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinCRM.UITest
{
    public class LeadContactPage : BasePage
    {
        readonly Query DetailsTab;
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
        readonly string Done;
        readonly string Save;
        readonly Query ContactList;

        public LeadContactPage(IApp app, Platform platform)
            : base(app, platform, "Phone", "Phone")
        {
            if (OnAndroid)
            {
                DetailsTab = x => x.Marked("Details");
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
                ContactList = x => x.Class("ListView");
                Done = "DONE";
                Save = "Save";
            }
            if (OniOS)
            {
                DetailsTab = x => x.Class("UITabBarButtonLabel").Text("Details");
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
                ContactList = x => x.Class("UITableViewWrapperView");
                Done = "DONE";
                Save = "Save";
            }
        }

        public void GoToLeadDetails()
        {
            if (OnAndroid)
                app.ScrollUpTo("Company");
            if (OniOS)
                app.Tap(DetailsTab);
        }

        public LeadContactPage EnterLeadContact(
            string role,
            string firstName,
            string lastName,
            string phone,
            string email,
            string address,
            string postalCode,
            string city,
            string state,
            string country)
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

            app.ScrollDown(ContactList);

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

            return this;
        }

        public void SaveLead()
        {
            app.Tap(Save);
            app.Tap(Save);
        }
    }
}

