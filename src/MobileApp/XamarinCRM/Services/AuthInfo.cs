using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace XamarinCRM.Services
{
    public class AuthInfo
    {
        readonly IConfigFetcher _ConfigFetcher;
        readonly MobileServiceClient _Client;

        static AuthInfo instance;

        public const MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;

        AuthInfo()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _Client = new MobileServiceClient(
                _ConfigFetcher.GetAsync("customerDataServiceUrl").Result,
                _ConfigFetcher.GetAsync("customerDataServiceAppKey", true).Result);
        }

        public static AuthInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthInfo();
                }
                return instance;
            }
        }

        public MobileServiceClient GetMobileServiceClient()
        {
            return _Client;
        }
    }
}