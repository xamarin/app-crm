
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Account : Contact
    {
      public Account()
        : base()
      {
        ContactId = Notes = string.Empty;
        //Industry = IndustryTypes[IndustryTypes.Length - 1];
        Industry = IndustryTypes[0];
        OpportunityStage = OpportunityStages[0];
      }

      [JsonProperty(PropertyName = "contactid")]
      public string ContactId { get; set; }

      [JsonProperty(PropertyName = "is_lead")]
      public bool IsLead { get; set; }

      [JsonProperty(PropertyName = "industry")]
      public string Industry { get; set; }

      [JsonProperty(PropertyName = "oppt_size")]
      public double OpportunitySize { get; set; }

      [JsonProperty(PropertyName = "oppt_stage")]
      public string OpportunityStage { get; set; }

      [JsonProperty(PropertyName="notes")]
      public string Notes { get; set; }

      [JsonIgnore]
      public static string[] IndustryTypes = new string[] { "None Selected", "Aerospace", "Education", "Electrical", "Entertainment", "Financial Services", "Logistic", "Healthcare", "Manufacturing", "Retail", "Other" };

      [JsonIgnore]
      public static string[] OpportunityStages = new string[] { "None Selected", "10% - Prospect", "50% - Value Proposition", "75% - Proposal" }; 

    }
}
