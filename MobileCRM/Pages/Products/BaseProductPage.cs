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

        #region implemented abstract members of BaseContentPage

        /// <summary>
        /// Place any code in the overidden method that should execute only if the app is currently authenticated, like
        /// fetching data to update the view, etc.
        /// Empty here in BaseProductPage, because we currently don't require authentication in order to call the Products Web API.
        /// </summary>
        protected override void ExecuteOnlyIfAuthenticated() { }

        #endregion
    }
}

