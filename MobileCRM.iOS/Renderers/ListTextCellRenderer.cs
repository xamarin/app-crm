using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MobileCRM.iOS.Renderers;
using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared.Helpers;

[assembly: ExportCell(typeof(ListTextCell), typeof(ListTextCellRenderer))]

namespace MobileCRM.iOS.Renderers
{
  public class ListTextCellRenderer : TextCellRenderer
  {
    public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableView tv)
    {

      var cell = base.GetCell(item, tv);

      cell.Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator;


      cell.BackgroundColor = Color.Transparent.ToUIColor();


      cell.TextLabel.TextColor = AppColors.LABELWHITE.ToUIColor();

      cell.DetailTextLabel.TextColor = AppColors.LABELBLUE.ToUIColor();

      tv.SeparatorColor = AppColors.SEPARATOR.ToUIColor();

      return cell;
    }

  }

}