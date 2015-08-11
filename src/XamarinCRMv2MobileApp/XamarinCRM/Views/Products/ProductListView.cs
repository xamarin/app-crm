using XamarinCRM.Cells;
using Xamarin.Forms;
using XamarinCRM.Views.Base;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Products
{
    public class ProductListView : BaseNonPersistentSelectedItemListView
    {
        public ProductListView()
        {
            ItemTemplate = new DataTemplate(typeof(ProductListItemCell));
            HasUnevenRows = false;
            RowHeight = (int)Sizes.LargeRowHeight;
            SeparatorVisibility = SeparatorVisibility.Default;
            SeparatorColor = Palette._013;
        }
    }
}


