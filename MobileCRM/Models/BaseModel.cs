using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace MobileCRM.Models
{
    public class BaseModel
    {
      public BaseModel()
      {
        
      }

      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }

      [Version]
      public string Version { get; set; }
    }
}
