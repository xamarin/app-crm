using MobileCRM.Cells;
using Xamarin.Forms;
using MobileCRM.Views.Base;
using MobileCRM.Statics;

namespace MobileCRM.Views.Products
{
    public class ProductListView : BaseNonPersistentSelectedItemListView
    {
        public ProductListView()
        {
            ItemTemplate = new DataTemplate(typeof(ProductListItemCell));
            HasUnevenRows = false;
            RowHeight = (int)Sizes.LargeRowHeight;
            SeparatorVisibility = SeparatorVisibility.Default;
            SeparatorColor = Palette._012;
        }
    }
}


