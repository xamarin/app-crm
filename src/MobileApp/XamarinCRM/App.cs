using System;
using System.Threading.Tasks;
using XamarinCRM.Localization;
using XamarinCRM.Pages;
using XamarinCRM.Services;
using Xamarin.Forms;
using Connectivity.Plugin;

namespace XamarinCRM
{
    public class App : Application
    {
        static Page _RootPage;

        public App()
        {
            SetCulture();

            _RootPage = new RootPage();

            MainPage = _RootPage;
        }

        static readonly Lazy<AuthenticationService> _LazyAuthenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService());

        static AuthenticationService _AuthenticationService { get { return _LazyAuthenticationService.Value; } }

        static void SetCulture()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public static async void ExecuteIfConnected(Action actionToExecuteIfConnected)
        {
            if (IsConnected)
            {
                await Task.Factory.StartNew(actionToExecuteIfConnected);
            }
            else
            {
                await ShowNetworkConnectionAlert();
            }
        }

        public static async Task ExecuteIfConnected(Func<Task> actionToExecuteIfConnected)
        {
            if (IsConnected)
            {
                await actionToExecuteIfConnected();
            }
            else
            {
                await ShowNetworkConnectionAlert();
            }
        }

        static async Task ShowNetworkConnectionAlert()
        {
            await _RootPage.DisplayAlert(
                TextResources.NetworkConnection_Alert_Title, 
                TextResources.NetworkConnection_Alert_Message, 
                TextResources.NetworkConnection_Alert_Confirm);
        }

        public static bool IsConnected
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }
    }
}
