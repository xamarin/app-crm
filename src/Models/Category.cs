#if SERVICE
using Microsoft.WindowsAzure.Mobile.Service;
#endif

namespace XamarinCRM.Models
{
    public class Category : 
#if SERVICE
        EntityData
#else
        BaseModel
#endif
    {
        public Category()
        {
            Name = Description = ImageUrl = ParentCategoryId = string.Empty;
            Sequence = 0;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ParentCategoryId { get; set; }

        public bool HasSubCategories { get; set; }

        public int Sequence { get; set; }
    }
}
