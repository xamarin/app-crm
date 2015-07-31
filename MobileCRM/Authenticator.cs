using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.Extensions;

namespace MobileCRM
{
    public class Authenticator
    {
        // ideally, these should all be loaded from a config file
        const string clientId = "591ddccc-4dd3-46d8-af4e-7886eb674cb9";
        const string tenantAuthority = "https://login.windows.net/xamcrm.onmicrosoft.com";
        static readonly Uri returnUri = new Uri("https://xamarin.com");
        const string resourceUri = "https://graph.windows.net";

        static AuthenticationResult _AuthenticationResult;

        AuthenticationContext _AuthenticationContext;

        public Authenticator()
        {
            _AuthenticationContext = new AuthenticationContext(tenantAuthority);
        }

        public async Task Authenticate(IPlatformParameters platformParameters)
        {
            try
            {
                _AuthenticationResult = await _AuthenticationContext.AcquireTokenAsync(resourceUri, clientId, returnUri, platformParameters);
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

