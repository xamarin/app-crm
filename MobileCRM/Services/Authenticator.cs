using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.Extensions;
using Xamarin.Forms;

namespace MobileCRM.Services
{
    public class Authenticator
    {
        readonly string _ClientId;
        readonly string _TenantAuthority;
        readonly Uri _ReturnUri;
        readonly string _ResourceUri;

        static AuthenticationResult _AuthenticationResult;

        AuthenticationContext _AuthenticationContext;

        readonly IConfigFetcher _ConfigFetcher;

        public Authenticator()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _ClientId = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationClientId", true).Result;
            _TenantAuthority = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationTenantAuthorityUrl").Result;
            _ReturnUri = new Uri( _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationReturnUri").Result);
            _ResourceUri = _ConfigFetcher.GetAsync("azureActiveDirectoryAuthenticationResourceUri").Result;

            _AuthenticationContext = new AuthenticationContext(_TenantAuthority);
        }

        public async Task Authenticate(IPlatformParameters platformParameters)
        {
            try
            {
                _AuthenticationResult = await _AuthenticationContext.AcquireTokenAsync(_ResourceUri, _ClientId, _ReturnUri, platformParameters);
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
            }
        }

        public async Task Logout()
        {
            try
            {
                await Task.Factory.StartNew(() => { 
                    _AuthenticationContext.TokenCache.Clear();
                    _AuthenticationResult = null; 
                });
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
            }
        }

        public bool IsAuthenticated
        {
            get { return _AuthenticationResult != null; }
        }
    }
}

