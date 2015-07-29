using Xamarin.Forms;
using MobileCRM.Views.Base;

namespace MobileCRM.Views.Sales
{
    public class LeadListView : BaseNonPersistentSelectedItemListView
    {
        public LeadListView()
        {
            HasUnevenRows = false; // Circumvents calculating heights for each cell individually. The rows of this list view will have a static height.
            RowHeight = 60; // set the row height for the list view items
            SeparatorVisibility = SeparatorVisibility.None; // make the row separators invisible, per the intended design of this app
        }
    }
}
