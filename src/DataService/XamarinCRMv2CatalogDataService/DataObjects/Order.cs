using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace XamarinCRMv2DataService.DataObjects
{
    public class Order : EntityData
    {
        public bool IsOpen { get; set; }
        public string AccountId { get; set; }
        public double Price { get; set; }
        public string Item { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}
