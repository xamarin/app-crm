using Microsoft.WindowsAzure.Mobile.Service;

namespace XamarinCRMv2DataService.DataObjects
{
    public class Category : EntityData
    {
        public virtual string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ParentCategoryId { get; set; }
        public int Sequence { get; set;}
        public bool HasSubCategories { get; set; }
    }
}