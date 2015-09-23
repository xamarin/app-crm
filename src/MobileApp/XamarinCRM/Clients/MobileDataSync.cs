using Xamarin.Forms;
using XamarinCRM.Services;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinCRM.Clients
{
    public class MobileDataSync
    {
        readonly IConfigFetcher _ConfigFetcher;

        readonly MobileServiceClient _Client;

        static MobileDataSync _Instance;

        public MobileDataSync()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            var serviceUrl = _ConfigFetcher.GetAsync("dataServiceUrl").Result;
            var serviceAppKey = _ConfigFetcher.GetAsync("dataServiceAppKey", true).Result;

            _Client = new MobileServiceClient(serviceUrl, serviceAppKey);
        }

        public static MobileDataSync Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MobileDataSync();
                }
                return _Instance;
            }
        }

        public MobileServiceClient GetMobileServiceClient()
        {
            return _Client;
        }
    }
}

