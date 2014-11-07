using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared;
using MobileCRMAndroid;
using Android.Widget;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using Android.Graphics;

using Color = Xamarin.Forms.Color;
using View = global::Android.Views.View;
using ViewGroup = global::Android.Views.ViewGroup;
using Context = global::Android.Content.Context;
using ListView = global::Android.Widget.ListView;
using Android.App;

[assembly: ExportCell(typeof(ListTextCell), typeof(ListTextCellRenderer))]

namespace MobileCRMAndroid
{
    public class ListTextCellRenderer : TextCellRenderer
    {


        protected override View GetCellCore (Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = (LinearLayout)base.GetCellCore (item, convertView, parent, context);
            cell.SetPadding(20, 10, 0, 10);
            cell.DividerPadding = 50;

            var div = new ShapeDrawable();
            div.SetIntrinsicHeight(1);

            div.Paint.Set(new Paint { Color = MobileCRM.Shared.Helpers.AppColors.SEPARATOR.ToAndroid() });


            if (parent is ListView) {
                ((ListView)parent).Divider = div;
                ((ListView)parent).DividerHeight = 1;
            }


            var label = (TextView)((LinearLayout)cell.GetChildAt(1)).GetChildAt(0);


            label.SetTextColor(MobileCRM.Shared.Helpers.AppColors.LABELWHITE.ToAndroid());

            
            label.TextSize = Font.SystemFontOfSize(NamedSize.Large).ToScaledPixel();

            var secondaryLabel = (TextView)((LinearLayout)cell.GetChildAt(1)).GetChildAt(1);

            secondaryLabel.SetTextColor(MobileCRM.Shared.Helpers.AppColors.LABELBLUE.ToAndroid());
           
            secondaryLabel.TextSize = Font.SystemFontOfSize(NamedSize.Medium).ToScaledPixel();


            return cell;
        }
    }
}

