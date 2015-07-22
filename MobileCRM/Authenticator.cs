using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;

namespace MobileCRM
{
    public static class Authenticator
    {
        public static readonly string clientId = "a5d92493-ae5a-4a9f-bcbf-9f1d354067d3";
        public static readonly string commonAuthority = "https://login.windows.net/common";
        public static readonly string tenantAuthority = "https://login.windows.net/xamcrm.onmicrosoft.com";
        public static readonly Uri returnUri = new Uri("http://MobileCRM");
        const string graphResourceUri = "https://graph.windows.net";
        public static readonly string graphApiVersion = "2013-11-08";

        public static async Task Authenticate(IPlatformParameters platformParameters)
        {
            AuthenticationResult authResult = null;

            // To avoid the user consent page, input the values for your registered application above,
            // comment out the if statement immediately below, and replace the commonAuthority parameter
            // with https://login.windows.net/<your.tenant.domain.com>
            AuthenticationContext authContext = new AuthenticationContext(tenantAuthority);
//            if (authContext.TokenCache.ReadItems().Any())
//                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, platformParameters);
        }
    }
}

