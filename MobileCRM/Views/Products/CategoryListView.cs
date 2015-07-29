using MobileCRM.Cells;
using Xamarin.Forms;
using MobileCRM.Views.Base;

namespace MobileCRM.Views.Products
{
    public class CategoryListView : BaseNonPersistentSelectedItemListView
    {
        public CategoryListView()
        {
            ItemTemplate = new DataTemplate(typeof(CategoryListItemCell));
            HasUnevenRows = false;
            RowHeight = 60;
            SeparatorVisibility = SeparatorVisibility.Default;
            SeparatorColor = Palette._012;
        }
    }
}


