using System;
using MobileCRM.Pages.Base;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MobileCRM
{
    public abstract class BaseProductPage : ContentPage
    {
        protected BaseProductPage(bool showNavigationBar = false)
        {
            // hide the navigstion bar on Android
            Device.OnPlatform(Android: () => NavigationPage.SetHasNavigationBar(this, showNavigationBar));
        }
    }
}

