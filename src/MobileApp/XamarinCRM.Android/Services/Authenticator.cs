
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;
using Xamarin.Forms;
using XamarinCRM.Extensions;
using XamarinCRMAndroid.Services;
using XamarinCRM.Services;

[assembly: Dependency(typeof(Authenticator))]

namespace XamarinCRMAndroid.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters((Activity)Forms.Context);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
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
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
                return false;
            }
            return true;
        }

        public async Task<string> FetchToken(string authority)
        {
            try
            {
                return 
                    (new AuthenticationContext(authority))
                        .TokenCache
                        .ReadItems()
                        .FirstOrDefault(x => x.Authority == authority).AccessToken;
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(this);
            }
            return null;
        }
    }
}

