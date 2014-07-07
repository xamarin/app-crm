
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Account : BaseModel
    {
      public Account()
        : base()
      {
        Industry = ContactId  = string.Empty;
      }

      [JsonProperty(PropertyName = "contactid")]
      public string ContactId { get; set; }

      [JsonProperty(PropertyName = "is_lead")]
      public bool IsLead { get; set; }

      [JsonProperty(PropertyName = "industry")]
      public string Industry { get; set; }


      public override string ToString()
      {
        return Company;
      }
      
    }
}
