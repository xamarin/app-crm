using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Shared.Attributes;
using Newtonsoft.Json;


namespace MobileCRM.Shared.Models
{
    public class BaseModel
    {
      public BaseModel()
      {
        
      }


      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }

      [Microsoft.WindowsAzure.MobileServices.Version]
      public string Version { get; set; }

    }
}
