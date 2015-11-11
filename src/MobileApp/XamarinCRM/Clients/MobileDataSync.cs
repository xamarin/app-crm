// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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

