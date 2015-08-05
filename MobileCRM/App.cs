using MobileCRM.Localization;
using MobileCRM.Pages;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.Services;
using MobileCRM.Extensions;

namespace MobileCRM
{
    public class App : Application
    {
        static readonly Lazy<AuthenticationService> _LazyAuthenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService());

        static AuthenticationService _AuthenticationService { get { return _LazyAuthenticationService.Value; } }

        public static async Task<bool> Authenticate()
        {
            try
            {
                return await _AuthenticationService.Authenticate();
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(typeof(App));
                return false;
            }
        }

        public static async Task<bool> Logout()
        {
            try
            {
                return await _AuthenticationService.Logout();
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(typeof(App));
                return false;
            }
        }

        public static bool IsAuthenticated
        {
            get { return _AuthenticationService.IsAuthenticated; }
        }

        public App()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        
            MainPage = new RootPage();
        }
    }
}
