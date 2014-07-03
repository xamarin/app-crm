using Xamarin.Forms.Platform.iOS;
using MobileCRM.Shared.CustomViews;
using Xamarin.Forms;
using MonoTouch.UIKit;
using MobileCRM.iOS;

[assembly: ExportCell (typeof (MenuCell), typeof (MenuCellRenderer))]

namespace MobileCRM.iOS
{

    public class MenuCellRenderer : ImageCellRenderer
    {
        public override UITableViewCell GetCell (Cell item, UITableView tv)
        {
            var cellView = base.GetCell (item, tv);

            cellView.BackgroundColor = Color.Transparent.ToUIColor();
            cellView.TextLabel.TextColor = Color.FromHex("FFFFFF").ToUIColor();
            cellView.DetailTextLabel.TextColor = Color.FromHex("AAAAAA").ToUIColor();

            tv.SeparatorColor = Color.FromHex("444444").ToUIColor();

            return cellView;
        }
    }
    
}
