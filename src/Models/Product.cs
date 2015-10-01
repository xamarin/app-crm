#if SERVICE
using Microsoft.WindowsAzure.Mobile.Service;
#endif

namespace XamarinCRM.Models
{
    public class Product :
#if SERVICE
        EntityData
#else
        BaseModel
#endif
    {
        public Product()
        {
            Name = Description = ImageUrl = string.Empty;
            Price = 0;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }
    }
}
