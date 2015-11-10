//
//   Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific langwuage governing permissions and
//    limitations under the License.

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
