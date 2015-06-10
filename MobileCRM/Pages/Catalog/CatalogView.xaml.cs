using Xamarin;
using MobileCRM.ViewModels.Catalog;

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
