using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Job : BaseModel
    {
      public bool IsArchived { get; set; }
      public bool IsProposed { get; set; }
      public string AccountId { get; set; }
    }
}
