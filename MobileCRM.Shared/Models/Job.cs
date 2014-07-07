using MobileCRM.Shared.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Job : BaseModel
    {
      public Job()
        : base()
      {
        JobType = AccountId = Source  = string.Empty;
      }

      [JsonProperty(PropertyName = "is_archived")]
      public bool IsArchived { get; set; }

      [JsonProperty(PropertyName = "is_proposed")]
      public bool IsProposed { get; set; }

      [JsonProperty(PropertyName="job_type")]
      public string JobType { get; set; }

      [JsonProperty(PropertyName = "accountid")]
      public string AccountId { get; set; }

      [JsonProperty(PropertyName = "source")]
      public string Source { get; set; }

      [Display("Qualified?")]
      [JsonProperty(PropertyName = "is_qualified")]
      public bool IsQualified { get; set; }

      [Display("Est. Amount"), Currency]
      [JsonProperty(PropertyName = "estimated_amount")]
      public int EstimatedAmount { get; set; }

      public override string ToString()
      {
        return string.Format("{0}{1}", Company, EstimatedAmount == Decimal.Zero
            ? string.Empty
            : string.Format(" - {0:C0}{1}", EstimatedAmount / 1000, EstimatedAmount > 1000 ? "K" : string.Empty)
        );
      }
    }
}
