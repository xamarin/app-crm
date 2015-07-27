using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MobileCRM.Pages.Base;
using MobileCRM.iOS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(BaseContentPageRenderer))]

namespace MobileCRM.iOS
{
    public class BaseContentPageRenderer : PageRenderer
    {
        BaseContentPage _Page;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            _Page = e.NewElement as BaseContentPage;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _Page.PlatformParameters = new PlatformParameters(this);
        }
    }
}

