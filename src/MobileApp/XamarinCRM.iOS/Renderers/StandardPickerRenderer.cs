//
//  Copyright 2015 Xamarin, Inc.
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
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (Picker), typeof (XamarinCRM.iOS.StandardPickerRenderer))]
namespace XamarinCRM.iOS
{
	public class StandardPickerRenderer : PickerRenderer
	{
		UIView paddingView;

		protected override void OnElementChanged (ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			try {
				// Add left padding to UITextField
				paddingView = new UIView (new CoreGraphics.CGRect (0, 0, 15, Control.Frame.Height));
				Control.LeftView = paddingView;
				Control.LeftViewMode = UITextFieldViewMode.Always;

				Control.BorderStyle = UITextBorderStyle.None;
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex);
			}
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			paddingView.Dispose ();
			paddingView = null;
		}
	}
}