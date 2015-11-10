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
using System.Threading.Tasks;
using XamarinCRM.Pages;
using Xamarin.Forms;
using Connectivity.Plugin;
using XamarinCRM.Localization;
using XamarinCRM.Services;
using XamarinCRM.Pages.Splash;

namespace XamarinCRM
{
    public class App : Application
    {
        static Application app;
        public static Application CurrentApp
        {
            get { return app; }
        }

        readonly IAuthenticationService _AuthenticationService;
        public App()
        {
            app = this;
            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            /* if we were targeting Windows Phone, we'd want to include the next line. */
            // if (Device.OS != TargetPlatform.WinPhone) 
            TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();


            // If the App.IsAuthenticated property is false, modally present the SplashPage.
            if (!_AuthenticationService.IsAuthenticated)
            {
                MainPage = new SplashPage();
            }
            else
            {
                GoToRoot();
            }

        }

        public static void GoToRoot()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                CurrentApp.MainPage = new RootTabPage();
            }
            else
            {
                CurrentApp.MainPage = new RootPage();
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
            await CurrentApp.MainPage.DisplayAlert(
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
