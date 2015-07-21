using Xamarin.Forms;
using MobileCRM.Cells;

namespace MobileCRM.Views.Products
{
    public class ProductListView : ListView
    {
        public ProductListView()
        {
            ItemTemplate = new DataTemplate(typeof(ProductListItemCell));
            HasUnevenRows = false;
            RowHeight = 60;
            SeparatorVisibility = SeparatorVisibility.Default;
            SeparatorColor = Palette._012;
        }
    }
}


