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

        private MobileDataSync()
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

