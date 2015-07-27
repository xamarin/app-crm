using System;
using Xamarin.Forms.Platform.Android;
using MobileCRM.Pages.Base;
using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;
using MobileCRMAndroid;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(BaseContentPageRenderer))]

namespace MobileCRMAndroid
{
    public class BaseContentPageRenderer : PageRenderer
    {
        BaseContentPage _Page;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            _Page = e.NewElement as BaseContentPage;

            _Page.PlatformParameters = new PlatformParameters((Activity)this.Context);
        }
    }
}

