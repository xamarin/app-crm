// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using XamarinCRM.Services;
using Xamarin;
using System.Collections.Generic;
using Microsoft.Azure.ActiveDirectory.GraphClient;

[assembly: Dependency(typeof(AuthenticationService))]

namespace XamarinCRM.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        string _AzureAuthenticationClientId;
        string _AzureGraphApiClientId;
        string _TenantAuthority;
        string _ReturnUri;
        string _ResourceUri;

        readonly IAuthenticator _Authenticator;

        readonly IConfigFetcher _ConfigFetcher;

        AuthenticationResult _AuthenticationResult;

        public AuthenticationService()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _Authenticator = DependencyService.Get<IAuthenticator>();
        }

        public async Task<bool> AuthenticateAsync()
        {
            await GetConfigAsync();

            // prompts the user for authentication
            _AuthenticationResult = await _Authenticator.Authenticate(_TenantAuthority, _ResourceUri, _AzureAuthenticationClientId, _ReturnUri);

            var accessToken = await GetTokenAsync();

            // instantiate an ActiveDirectoryClient to query the Graph API
            var activeDirectoryGraphApiClient = new ActiveDirectoryClient(
                new Uri(new Uri(_ResourceUri), _AzureGraphApiClientId),
                () => Task.FromResult<string>(accessToken)
            );

            // query the Azure Graph API for some detailed user information about the logged in user
            //This is done differently based on platform because if this is not awaited in iOS, it crashes
            //the app. Android is done this way to correct login issues that were previously occurring.
            if (Xamarin.Forms.Device.OS == TargetPlatform.Android)
            {
                Task.Run(() =>
                    {
                        LogUserInfo(activeDirectoryGraphApiClient);
                    });
            }
            else
            {
                await Task.Run(async () =>
                    {
                        LogUserInfo(activeDirectoryGraphApiClient);
                    });
            }

            return true;
        }

        void LogUserInfo(ActiveDirectoryClient activeDirectoryGraphApiClient)
        {
            var user = activeDirectoryGraphApiClient.Me.ExecuteAsync().Result;
            // record some info about the logged in user with Xamarin Insights
            if (user != null)
            {
                Insights.Identify(
                    _AuthenticationResult.UserInfo.UniqueId, 
                    new Dictionary<string, string>
                    {
                        { Insights.Traits.Email, user.UserPrincipalName },
                        { Insights.Traits.FirstName, user.GivenName },
                        { Insights.Traits.LastName, user.Surname },
                        { "Preferred Language", user.PreferredLanguage }
                    }
                );
            }
        }

        public async Task<bool> LogoutAsync()
        {
            await GetConfigAsync();

            await Task.Factory.StartNew(async () =>
                { 
                    var success = await _Authenticator.DeAuthenticate(_TenantAuthority);
                    if (!success)
                    {
                        throw new Exception("Failed to DeAuthenticate!");
                    }
                    _AuthenticationResult = null;
                });
            return true;
        }

        public async Task<string> GetTokenAsync()
        {
            await GetConfigAsync();

            return await _Authenticator.FetchToken(_TenantAuthority);
        }

        public bool IsAuthenticated
        {
            get 
            { 
                if (_AuthenticationBypassed)
                    return true;
                else
                    return _AuthenticationResult != null;
            }
        }

        bool _AuthenticationBypassed;
        /// <summary>
        /// Used only for demo, to quickly bypass actual authentication
        /// </summary>
        /// <returns>Task</returns>
        public void BypassAuthentication()
        {
            _AuthenticationBypassed = true;
        }

        async Task GetConfigAsync()
        {
            if (_AzureAuthenticationClientId == null)
                _AzureAuthenticationClientId = await _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationClientId", true);

            if (_AzureGraphApiClientId == null)
                _AzureGraphApiClientId = await _ConfigFetcher.GetAsync("azureActiveDirectoryGraphApiClientId", true);

            if (_TenantAuthority == null)
                _TenantAuthority = await _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationTenantAuthorityUrl");

            if (_ReturnUri == null)
                _ReturnUri = await _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationReturnUri");

            if (_ResourceUri == null)
                _ResourceUri = await _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationResourceUri");
        }
    }
}

