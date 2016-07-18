

// The model class files are shared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

namespace XamarinCRM.Models
{
    public class Category : BaseModel
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
