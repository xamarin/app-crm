using Xamarin.Forms.Platform.iOS;
using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared.Helpers;
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

            //cellView.TextLabel.TextColor = Color.FromHex("FFFFFF").ToUIColor();
            cellView.TextLabel.TextColor = AppColors.LABELWHITE.ToUIColor();
            
            //cellView.DetailTextLabel.TextColor = Color.FromHex("AAAAAA").ToUIColor();
            cellView.DetailTextLabel.TextColor = AppColors.LABELBLUE.ToUIColor();

            //tv.SeparatorColor = Color.FromHex("444444").ToUIColor();
            tv.SeparatorColor = AppColors.SEPARATOR.ToUIColor();

            return cellView;
        }
    }
    
}
