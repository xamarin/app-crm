

// The model class files are shared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

namespace XamarinCRM.Models
{
    public class Product : BaseModel
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

        #if !SERVICE

        public string ThumbnailImageUrl 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(ImageUrl) || !ImageUrl.Contains("."))
                    return null;

                var index = ImageUrl.LastIndexOf('.');
                var name = ImageUrl.Substring(0, index);
                var extension = ImageUrl.Substring(index);
                return string.Format("{0}-thumb{1}", name, extension);
            }
        }

        #endif
    }
}
