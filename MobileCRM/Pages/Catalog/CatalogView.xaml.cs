using MobileCRM.ViewModels.Catalog;
using Xamarin;

namespace MobileCRM.Pages.Catalog
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
