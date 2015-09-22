using Xamarin.Forms;
using XamarinCRM.Services;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinCRM
{
    public class MobileDataSync
    {
        readonly IConfigFetcher _ConfigFetcher;

        readonly MobileServiceClient _Client;

        static MobileDataSync _Instance;

        public MobileDataSync()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _Client = new MobileServiceClient(
                _ConfigFetcher.GetAsync("catalogDataServiceUrl").Result,
                _ConfigFetcher.GetAsync("catalogDataServiceAppKey", true).Result);
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

