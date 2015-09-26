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
            /* if we were targeting Windows Phone, we'd want to include the next line. */
            // if (Device.OS != TargetPlatform.WinPhone) 
            TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            _RootPage = new RootPage();

            MainPage = _RootPage;
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

        public static int AnimationSpeed = 250;
    }
}
