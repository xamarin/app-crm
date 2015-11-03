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
            var userFetcher = activeDirectoryGraphApiClient.Me.ToUser();
            var user = await userFetcher.ExecuteAsync();

            // record some info about the logged in user with Xamarin Insights
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

            return true;
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

