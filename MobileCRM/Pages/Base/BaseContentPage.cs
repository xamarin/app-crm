using System.Threading.Tasks;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
using Xamarin.Forms;

namespace MobileCRM.Pages.Base
{
    public abstract class BaseContentPage : ContentPage
    {
        /// <summary>
        /// Called immediately before showing the page.
        /// If the current user is not authenticated, the Content.IsVisible property is set to false, and the LoginPage is modally presented.
        /// If the current user IS authenticated, the Content.IsVisible is set to true;
        /// </summary>
        protected async override void OnAppearing()
        {
            await OnAppearingActions();
        }

        async Task OnAppearingActions()
        {
            base.OnAppearing();

            if (AuthInfo.Instance.User == null)
            {
                // Since the user is not authenticated, they should not be presented with any content for this page, 
                // not even for a second while the modal is animating. The content should be blank until authenticted.
                Content.IsVisible = false;

                LoginPage loginPage = new LoginPage();

                // This conditional for Android is necessary because OnAppearing is only
                // called when an Android Acvitiy is first shown, unlike iOS in which OnAppearing()
                // is called each time the view appears on screen, as the name suggests.
                // If Android, we manually re-call OnAppearing when the LoginPage modal is dismissed.
                if (Device.OS == TargetPlatform.Android)
                {
                    loginPage.Disappearing += async delegate
                        {
                            await OnAppearingActions();
                        };
                }

                await Navigation.PushModalAsync(loginPage);
            }
            else
            {
                Content.IsVisible = true;
            }
        }

        protected ActivityIndicator ActivityIndicator
        {
            get
            {
                return new ActivityIndicator
                {
                    IsRunning = true
                };
            }
        }
    }
}
