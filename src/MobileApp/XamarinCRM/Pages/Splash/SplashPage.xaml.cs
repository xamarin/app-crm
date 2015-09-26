using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using XamarinCRM.Services;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Splash;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace XamarinCRM.Pages.Splash
{
    public partial class SplashPage : SplashPageXaml
    {
        IAuthenticationService _AuthenticationService;

        public SplashPage()
        {
            InitializeComponent();

            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            SignInButton.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(SignInButtonTapped) 
                });

            SkipSignInButton.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(SkipSignInButtonTapped) 
                });

            InfoButton.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(InfoButtonTapped) 
                });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.LoadDemoCredentials();

            await Task.Delay(250);
            await SignInButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await SkipSignInButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await XamarinLogo.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await InfoButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
        }

        async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {
                // The underlying call behind App.Authenticate() calls the ADAL library, which presents the login UI and awaits success.
                success = await _AuthenticationService.Authenticate();
            }
            catch (Exception ex)
            {
                if (ex is AdalException && (ex as AdalException).ErrorCode == "authentication_canceled")
                {
                    // Do nothing, just duck the exception. The user just cancelled out of the login UI.
                }
            }
            finally
            {
                // When the App.Authenticate() returns, the login UI is hidden, regardless of success (for example, if the user taps "Cancel" in iOS).
                // This means the SplashPage will be visible again, so we need to make the sign in button clickable again by hiding the activity indicator (via the IsPresentingLoginUI property).
                ViewModel.IsPresentingLoginUI = false;
            }

            return success;
        }

        async void SignInButtonTapped()
        {
            await App.ExecuteIfConnected(async () =>
                {
                    // trigger the activity indicator through the IsPresentingLoginUI property on the ViewModel
                    ViewModel.IsPresentingLoginUI = true;

                    if (await Authenticate())
                    {
                        // Pop off the modally presented SplashPage.
                        // Note that we're not popping the ADAL auth UI here; that's done automatcially by the ADAL library when the Authenticate() method returns.
                        await ViewModel.PopModalAsync();

                        // Broadcast a message that we have sucessdully authenticated.
                        // This is mostly just for Android. We need to trigger Android to call the SalesDashboardPage.OnAppearing() method,
                        // because unlike iOS, Android does not call the OnAppearing() method each time that the Page actually appears on screen.
                        MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);
                    }
                });
        }

        async void SkipSignInButtonTapped()
        {
            _AuthenticationService.BypassAuthentication();
            await ViewModel.PopModalAsync();
        }

        async void InfoButtonTapped()
        {

        }
    }

    public partial class SplashPageXaml : ModelBoundContentPage<SplashViewModel>
    {
    }
}

