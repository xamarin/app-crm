
namespace XamarinCRM.Models
{
    public class CatalogProduct
    {
        public CatalogProduct()
        {
            Name = Description = ImageUrl = string.Empty;
            Price = 0;
            CatalogCategory = new CatalogCategory();
        }

        public string Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public CatalogCategory CatalogCategory { get; set; }
    }
}

