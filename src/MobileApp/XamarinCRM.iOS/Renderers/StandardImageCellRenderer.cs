// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(ImageCell), typeof(XamarinCRM.iOS.StandardImageCellRenderer))]
namespace XamarinCRM.iOS
{
	public class StandardImageCellRenderer : ImageCellRenderer
	{
		public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			var cell = base.GetCell (item, reusableCell, tv);
			Debug.WriteLine ("Style Id" + item.StyleId);
			switch (item.StyleId)
			{
			case "checkmark":
				cell.Accessory = UIKit.UITableViewCellAccessory.Checkmark;
				break;
			case "detail-button":
				cell.Accessory = UIKit.UITableViewCellAccessory.DetailButton;
				break;
			case "detail-disclosure-button":
				cell.Accessory = UIKit.UITableViewCellAccessory.DetailDisclosureButton;
				break;
			case "disclosure":
				cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
				break;
			default:
				cell.Accessory = UIKit.UITableViewCellAccessory.None;
				break;
			}
			return cell;
		}
	}
}
