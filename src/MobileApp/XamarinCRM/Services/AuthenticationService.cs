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
            get { return _AuthenticationResult != null; }
        }
    }
}

