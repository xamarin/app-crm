using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class SalesPageTests : AbstractSetup
    {
        public SalesPageTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void ExamineLead()
        {
            new SalesHomePage(app, platform)
                .ClickOnFirstLead();
            
            new LeadDetailsPage(app, platform)
                .GoToLeadContact();

            new LeadContactPage(app, platform)
               .GoToLeadDetails();
        }

        [Test]
        public void EditLead()
        {
            string company = "UITest Automation";
            string industry = "Education";
            string size = "808.15";
            string stage = "50% - Value Proposition";

            new SalesHomePage(app, platform)
                .ClickOnFirstLead();

            new LeadDetailsPage(app, platform)
                .EnterLeadDetails(company, industry, size, stage)
                .SaveLead();

            new SalesHomePage(app, platform)
                .RefreshLeads()
                .VerifyLeadPresent(company, industry, size, stage);
        }

        [Test]
        public void AddNewLead()
        {
            string company = "UITest Auto New Lead";
            string industry = "Education";
            string size = "808.15";
            string stage = "50% - Value Proposition";
            string role = "CEO";
            string firstName = "Fearless";
            string lastName = "Leader";
            string phone = "1234567890";
            string email = "test@xamarin.com";
            string address = "394 Pacific Ave";
            string postalCode = "94111";
            string city = "San Francisco";
            string state = "CA";
            string country = "USA";

            new SalesHomePage(app, platform)
                .AddNewLead();

            new LeadDetailsPage(app, platform)
                .EnterLeadDetails(company, industry, size, stage)
                .GoToLeadContact();

            new LeadContactPage(app, platform)
                .EnterLeadContact(role, 
                firstName, 
                lastName, 
                phone, 
                email, 
                address, 
                postalCode, 
                city, 
                state, 
                country)
                .SaveLead();

            new SalesHomePage(app, platform)
                .RefreshLeads()
                .VerifyLeadPresent(company, industry, size, stage);
        }
    }
}

