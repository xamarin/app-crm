using MobileCRM.Shared.Models;

namespace MobileCRM.Shared.ViewModels.Catalog
{
    public class CatalogViewModel : BaseViewModel
    {
        CatalogItem catalogItem;

        public CatalogViewModel(string catalogItem)
        {
            this.Title = "Product Catalog";
            this.Icon = "list.png";

            this.catalogItem = CatalogItem.CreateCatalogItem(catalogItem);
        }
        //end ctor

        public CatalogItem Product
        {
            get
            {
                return catalogItem;
            }
            set
            {
                catalogItem = value;
            }
        }

        public string SuggestedPrice
        {
            get
            {
                return "Suggested Price: $" + catalogItem.SuggestedPrice.ToString() + " USD";
            }
        }
    }
}