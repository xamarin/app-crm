using System;
using MobileCRM;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;
using System.Linq;
using Xamarin.Forms;
using MobileCRMAndroid;
using MobileCRM.Extensions;

[assembly: Dependency(typeof(Authenticator))]

namespace MobileCRMAndroid
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
    }
}

