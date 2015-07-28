using System;
using MobileCRM.Pages.Base;
using Xamarin.Forms;

namespace MobileCRM
{
    public abstract class BaseProductPage : BaseContentPage
    {
        protected BaseProductPage()
        {
            // hide the navigstion bar on Android
            Device.OnPlatform(Android: () => NavigationPage.SetHasNavigationBar(this, false)); 
        }
    }
}

