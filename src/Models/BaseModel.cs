

using System;

#if SERVICE
using Microsoft.Azure.Mobile.Server.Tables;

#endif

namespace XamarinCRM.Models
{
    // The model class files are shared between the mobile and service projects. 
    // If ITableData were compatible with PCL profile 78, the models could be in a PCL.

    public class BaseModel
#if SERVICE
        : ITableData
#endif
    {
        public string Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool Deleted { get; set; }

        public byte[] Version { get; set; }
    }
}