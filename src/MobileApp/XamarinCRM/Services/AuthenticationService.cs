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

[assembly: Dependency(typeof(AuthenticationService))]

namespace XamarinCRM.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly string _ClientId;
        readonly string _TenantAuthority;
        readonly string _ReturnUri;
        readonly string _ResourceUri;

        readonly IAuthenticator _Authenticator;

        readonly IConfigFetcher _ConfigFetcher;

        AuthenticationResult _AuthenticationResult;

        public AuthenticationService()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _Authenticator = DependencyService.Get<IAuthenticator>();

            _ClientId = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationClientId", true).Result;
            _TenantAuthority = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationTenantAuthorityUrl").Result;
            _ReturnUri = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationReturnUri").Result;
            _ResourceUri = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationResourceUri").Result;
        }

        public async Task<bool> Authenticate()
        {
            _AuthenticationResult = await _Authenticator.Authenticate(_TenantAuthority, _ResourceUri, _ClientId, _ReturnUri);
            return true;
        }

        public async Task<bool> Logout()
        {
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

        public bool IsAuthenticated
        {
            get 
            { 
                if (_Bypassed)
                    return true;
                else
                    return _AuthenticationResult != null;
            }
        }

        bool _Bypassed;
        /// <summary>
        /// Used only for demo, to quickly bypass actual authentication
        /// </summary>
        /// <returns>Task</returns>
        public void BypassAuthentication()
        {
            _Bypassed = true;
        }
    }
}

