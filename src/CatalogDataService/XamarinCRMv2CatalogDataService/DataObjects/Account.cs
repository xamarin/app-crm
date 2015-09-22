using Microsoft.WindowsAzure.Mobile.Service;

namespace XamarinCRMv2DataService.DataObjects
{
    public class Account : EntityData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsLead { get; set; }
        public string Industry { get; set; }
        public double OpportunitySize { get; set; }
        public string OpportunityStage { get; set; }
        public string ImageUrl { get; set; }
    }
}
