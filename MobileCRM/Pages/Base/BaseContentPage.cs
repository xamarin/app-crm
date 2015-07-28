using System.Threading.Tasks;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MobileCRM.Pages.Base
{
    public abstract class BaseContentPage : ContentPage
    {
        public IPlatformParameters PlatformParameters { get; set; }

        /// <summary>
        /// Called immediately before showing the page.
        /// If the current user is not authenticated, the Content.IsVisible property is set to false, and the LoginPage is modally presented.
        /// If the current user IS authenticated, the Content.IsVisible is set to true;
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!App.IsAuthenticated)
            {
                // Since the user is not authenticated, they should not be presented with any content for this page, 
                // not even for a second while the modal is animating. The content should be blank until authenticted.
                Content.IsVisible = false;

                await App.Authenticate(PlatformParameters);

                Content.IsVisible = true;

                ExecuteOnlyIfAuthenticated();
            }
            else
            {
                Content.IsVisible = true;

                ExecuteOnlyIfAuthenticated();
            }
        }

        /// <summary>
        /// Place any code in the overidden method that should execute only if the app is currently authenticated, like fetching data to update the view, etc.
        /// </summary>
        protected abstract void ExecuteOnlyIfAuthenticated();
    }
}
