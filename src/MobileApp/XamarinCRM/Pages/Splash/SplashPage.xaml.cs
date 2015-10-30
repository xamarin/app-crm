//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using XamarinCRM.Services;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Splash;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin;

namespace XamarinCRM.Pages.Splash 
{
    public partial class SplashPage : SplashPageXaml
    {
        readonly IAuthenticationService _AuthenticationService;

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

            // fetch the demo credentials
            await ViewModel.LoadDemoCredentials();

            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeed);

            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await SignInButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await SkipSignInButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);

            // Using Task.WhenAll() allows these two animations to be run in parallel.
            await Task.WhenAll(new []
                { 
                    // FadeTo() modifies the Opacity property of the given VisualElements over a given duration.
                    XamarinLogo.FadeTo(1, (uint)App.AnimationSpeed, Easing.SinIn), 
                    InfoButton.FadeTo(1, (uint)App.AnimationSpeed, Easing.SinIn) 
                });

            Insights.Track(InsightsReportingConstants.PAGE_SPLASH);
        }

        async Task<bool> Authenticate()
        {
            bool success = false;
            try
            {
                // The underlying call behind App.Authenticate() calls the ADAL library, which presents the login UI and awaits success.
                success = await _AuthenticationService.AuthenticateAsync();
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

            // Broadcast a message that we have sucessdully authenticated.
            // This is mostly just for Android. We need to trigger Android to call the SalesDashboardPage.OnAppearing() method,
            // because unlike iOS, Android does not call the OnAppearing() method each time that the Page actually appears on screen.
            MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);

            await ViewModel.PopModalAsync();
        }

        async void InfoButtonTapped()
        {
            // navigation to the About page will go here when About Page is completed.
        }
    }

    /// <summary>
    /// This class definition just gives us a way to reference ModelBoundContentPage<T> in the XAML of this Page.
    /// </summary>
    public partial class SplashPageXaml : ModelBoundContentPage<SplashViewModel>
    {
    }
}

