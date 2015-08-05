using System;
using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.Cells;
using MobileCRM.Views.Base;

namespace MobileCRM.Views.Customers
{
    public class CustomerOrderListView : BaseNonPersistentSelectedItemListView
    {
        public CustomerOrderListView()
        {
            HasUnevenRows = false; // Circumvents calculating heights for each cell individually. The rows of this list view will have a static height.
            RowHeight = (int)Sizes.LargeRowHeight; // set the row height for the list view items
            SeparatorVisibility = SeparatorVisibility.None;
            ItemTemplate = new DataTemplate(typeof(OrderListItemCell));
            SeparatorColor = Palette._013;
        }
    }
}

