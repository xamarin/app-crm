using System;
using XamarinCRM.Pages.Base;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinCRM.Pages.Products
{
    public abstract class BaseProductPage : ContentPage
    {
        protected BaseProductPage(bool isPerformingProductSelection = false)
        {
            // hide the navigstion bar on Android
            Device.OnPlatform(Android: () => NavigationPage.SetHasNavigationBar(this, isPerformingProductSelection));
        }
    }
}

