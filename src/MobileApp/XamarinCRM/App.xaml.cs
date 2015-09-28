using System;
using Xamarin.Forms;
using XamarinCRM.Pages;
using XamarinCRM.Localization;
using System.Threading.Tasks;
using Connectivity.Plugin;

namespace XamarinCRM
{
    public partial class App : Application
    {
        static Page _RootPage;

        public App()
        {
            InitializeComponent();

            if (Application.Current.Resources == null) {
                Application.Current.Resources = new ResourceDictionary();
            }

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

