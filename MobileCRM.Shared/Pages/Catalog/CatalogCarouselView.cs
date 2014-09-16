using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Catalog
{
    public class CatalogCarouselView : CarouselPage
    {

        public CatalogCarouselView() : base()
        {
            this.Title = "Product Catalog";


            this.Children.Add(new CatalogView() { Title = "First" });

            this.Children.Add(new CatalogView() { Title = "Second" });

        }


    }
}
