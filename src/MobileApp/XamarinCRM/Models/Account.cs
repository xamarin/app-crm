using System;
using System.Linq;
using Newtonsoft.Json;

namespace XamarinCRM.Models
{
    public class Account : BaseModel
    {
        public Account() : base()
        {
            FirstName = LastName = Company = Street = Unit = City = PostalCode = State = Country = Phone = JobTitle = Email = string.Empty;
            Industry = IndustryTypes[0];
            OpportunityStage = OpportunityStages[0];
            IsLead = true;
        }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "jobtitle")]
        public string JobTitle { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "is_lead")]
        public bool IsLead { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "oppt_size")]
        public double OpportunitySize { get; set; }

        [JsonProperty(PropertyName = "oppt_stage")]
        public string OpportunityStage { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public double OpportunityStagePercent
        { 
            get
            {
                if (OpportunityStages.Length != OpportunityStagePercentages.Length)
                    throw new IndexOutOfRangeException("The OpportunityStages array and the OpportunityStagePercentages array must be of equal length.");

                double result = 0;

                for (int i = 0; i < OpportunityStages.Length; i++)
                {
                    if (OpportunityStage == OpportunityStages[i])
                    {
                        result = OpportunityStagePercentages[i];
                        break;
                    }
                }

                return result;
            }
        }

        [JsonIgnore]
        public static string[] IndustryTypes = { "None Selected", "Aerospace", "Education", "Electrical", "Entertainment", "Financial Services", "Logistic", "Healthcare", "Manufacturing", "Retail", "Other" };

        [JsonIgnore]
        public int IndustryTypeCurrentIndex
        {
            get { return IndustryTypes.ToList().IndexOf(Industry); }
            set { Industry = IndustryTypes[value]; }
        }

        [JsonIgnore]
        public static string[] OpportunityStages = { "None Selected", "10% - Prospect", "50% - Value Proposition", "75% - Proposal" };

        [JsonIgnore]
        public int OpportunityStageCurrentIndex
        {
            get { return OpportunityStages.ToList().IndexOf(OpportunityStage); }
            set { OpportunityStage = OpportunityStages[value]; }
        }

        [JsonIgnore]
        public static double[] OpportunityStagePercentages = { 0d, 10d, 50d, 75d };

        public string DisplayContact
        {
            get { return FirstName + " " + LastName; }
        }

        [JsonIgnore]
        public string AddressString
        {
            get { return string.Format("{0}{1} {2} {3} {4}", Street, !string.IsNullOrWhiteSpace(Unit) ? Unit + ", " : string.Empty + ", ", !string.IsNullOrWhiteSpace(City) ? City + "," : string.Empty, State, PostalCode); }
        }

        [JsonIgnore]
        public string DisplayName
        {
            get { return this.ToString(); }
        }

        [JsonIgnore]
        public string CityState
        {
            get { return City + ", " + State; }
        }

        [JsonIgnore]
        public string CityStatePostal
        {
            get { return CityState + " " + PostalCode; }
        }

        [JsonIgnore]
        public string StatePostal
        {
            get { return State + " " + PostalCode; }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
