using Xamarin.Forms;
using MobileCRM.ViewModels.Catalog;
using MobileCRM.Models;

namespace MobileCRM.Pages.Catalog
{
    public class CatalogCarouselView : CarouselPage
    {
        public CatalogCarouselView()
            : base()
        {
            this.Title = "Product Catalog";

            this.Children.Add(new CatalogView(new CatalogViewModel(CatalogItem.ITEM_PAPER)));
            this.Children.Add(new CatalogView(new CatalogViewModel(CatalogItem.ITEM_INK)));
            this.Children.Add(new CatalogView(new CatalogViewModel(CatalogItem.ITEM_PRINTER)));
            this.Children.Add(new CatalogView(new CatalogViewModel(CatalogItem.ITEM_SCANNER)));
            this.Children.Add(new CatalogView(new CatalogViewModel(CatalogItem.ITEM_COMBO)));
        }
    }
}