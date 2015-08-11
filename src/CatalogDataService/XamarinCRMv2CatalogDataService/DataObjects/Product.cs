using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;

namespace XamarinCRMv2CatalogDataService.DataObjects
{
    public class Product : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public string CategoryId { get; set; }
        /// <summary>
        /// In many cases, it doesn't make sense to serialize a navigational property.
        /// But in this case, it will be useful to have the category data with the 
        /// product, in order to provide nicely grouped search results.
        /// </summary>
        public virtual Category Category { get; set; }
    }
}