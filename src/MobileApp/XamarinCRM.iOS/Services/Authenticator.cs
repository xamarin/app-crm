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
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using XamarinCRM.Extensions;
using XamarinCRM.Services;
using XamarinCRM.iOS.Services;

[assembly: Dependency(typeof(Authenticator))]

namespace XamarinCRM.iOS.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);

            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            // ensures that the currently presented viewcontroller is acquired, even a modally presented one
            while (topController.PresentedViewController != null) {
                topController = topController.PresentedViewController;
            }

            var platformParams = new PlatformParameters(topController);

            var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), platformParams);

            return authResult;
        }

        public async Task<bool> DeAuthenticate(string authority)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                await Task.Factory.StartNew(() => { 
                    authContext.TokenCache.Clear();
                });
                return true;
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
            }
            return false;
        }

        public async Task<string> FetchToken(string authority)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);

                var tokenItems = authContext.TokenCache.ReadItems();

                var token = tokenItems.FirstOrDefault(x => x.Authority == authority);

                return token.AccessToken;
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
            }
            return null;
        }
    }
}

