using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MobileCRM.Shared.ViewModels.Catalog;
using MobileCRM.Shared.Models;


namespace MobileCRM.Shared.Pages.Catalog
{
    public class CatalogCarouselView : CarouselPage
    {

        public CatalogCarouselView() : base()
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
