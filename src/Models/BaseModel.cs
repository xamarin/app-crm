using System;

namespace XamarinCRM.Models
{
#if !SERVICE
    public class BaseModel
    {
        public string Id { get; set; }

        public string Version { get; set; }
    }
#endif
}