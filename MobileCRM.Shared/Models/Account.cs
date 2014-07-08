
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
        Industry = IndustryTypes[IndustryTypes.Length - 1];
      }

      [JsonProperty(PropertyName = "contactid")]
      public string ContactId { get; set; }

      [JsonProperty(PropertyName = "is_lead")]
      public bool IsLead { get; set; }

      [JsonProperty(PropertyName = "industry")]
      public string Industry { get; set; }

      [JsonProperty(PropertyName="notes")]
      public string Notes { get; set; }

      [JsonIgnore]
      public static string[] IndustryTypes = new string[] { "Aerospace", "Education", "Electrical", "Entertainment", "Logistic", "Retail", "Software", "Other" };
    }
}
