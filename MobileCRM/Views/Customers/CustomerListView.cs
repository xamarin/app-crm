using System;
using Xamarin.Forms;
using MobileCRM.Views.Base;
using MobileCRM.Statics;

namespace MobileCRM.Customers
{
    public class CustomerListView : BaseNonPersistentSelectedItemListView
    {
        public CustomerListView()
        {
            HasUnevenRows = false; // Circumvents calculating heights for each cell individually. The rows of this list view will have a static height.
            RowHeight = (int)Sizes.LargeRowHeight; // set the row height for the list view items
            SeparatorVisibility = SeparatorVisibility.Default; // make the row separators invisible, per the intended design of this app
        }
    }
}

