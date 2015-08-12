using System;
using Xamarin.Forms;
using XamarinCRM.Views.Base;
using XamarinCRM.Statics;
using XamarinCRM.Cells;

namespace XamarinCRM.Views.Customers
{
    public class CustomerListView : BaseNonPersistentSelectedItemListView
    {
        public CustomerListView()
        {
            HasUnevenRows = false; // Circumvents calculating heights for each cell individually. The rows of this list view will have a static height.
            RowHeight = (int)Sizes.LargeRowHeight; // set the row height for the list view items
            ItemTemplate = new DataTemplate(typeof(CustomerListItemCell));
            SeparatorColor = Palette._015;
        }
    }
}

