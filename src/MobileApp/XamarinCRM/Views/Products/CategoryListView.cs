using XamarinCRM.Cells;
using Xamarin.Forms;
using XamarinCRM.Views.Base;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Products
{
    public class CategoryListView : BaseNonPersistentSelectedItemListView
    {
        public CategoryListView()
        {
            ItemTemplate = new DataTemplate(typeof(CategoryListItemCell));
            RowHeight = (int)Sizes.LargeRowHeight;
            SeparatorVisibility = SeparatorVisibility.Default;
            SeparatorColor = Palette._013;
        }
    }
}


