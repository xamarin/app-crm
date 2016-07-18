

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
				// TODO: log error
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