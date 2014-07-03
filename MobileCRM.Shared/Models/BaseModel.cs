using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;


namespace MobileCRM.Shared.Models
{
    public class BaseModel
    {

      public string Id { get; set; }

      [Version]
      public string Version { get; set; }
    }
}
