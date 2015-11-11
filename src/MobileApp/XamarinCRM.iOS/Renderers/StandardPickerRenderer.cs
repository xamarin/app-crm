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