using MobileCRM.Shared.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Contact : BaseModel
    {
      public Contact() : base()
      {
        FirstName = LastName = Company  = string.Empty;
      }
      [JsonProperty(PropertyName = "firstname")]
      public string FirstName { get; set; }

      [JsonProperty(PropertyName = "lastname")]
      public string LastName { get; set; }

      public override string ToString()
      {
        return FirstName + " " + LastName;
      }
    }
}
