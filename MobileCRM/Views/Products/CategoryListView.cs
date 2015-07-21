using MobileCRM.Cells;
using Xamarin.Forms;

namespace MobileCRM.Views.Products
{
    public class CategoryListView : ListView
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


