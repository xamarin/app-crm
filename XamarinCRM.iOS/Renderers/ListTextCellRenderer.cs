using MobileCRM.CustomControls;
using MobileCRM.Helpers;
using MobileCRM.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportCell(typeof(ListTextCell), typeof(ListTextCellRenderer))]

namespace MobileCRM.iOS.Renderers
{
    public class ListTextCellRenderer : TextCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            cell.BackgroundColor = Color.Transparent.ToUIColor();

            cell.TextLabel.TextColor = AppColors.LABELWHITE.ToUIColor();

            cell.DetailTextLabel.TextColor = AppColors.LABELBLUE.ToUIColor();

            tv.SeparatorColor = AppColors.SEPARATOR.ToUIColor();

            return cell;
        }
    }
}