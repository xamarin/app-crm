using Microsoft.WindowsAzure.Mobile.Service;

namespace XamarinCRMv2DataService.DataObjects
{
    public class Product : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryId { get; set; }
    }
}