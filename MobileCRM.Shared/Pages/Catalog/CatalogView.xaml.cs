using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin;
using MobileCRM.Shared.ViewModels.Catalog;


namespace MobileCRM.Shared.Pages.Catalog
{
    public partial class CatalogView
    {
        public CatalogView(CatalogViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Insights.Track("Product Catalog Page");
        }
    }
}
