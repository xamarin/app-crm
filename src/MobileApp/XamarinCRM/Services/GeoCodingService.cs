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
using System;
using XamarinCRM.Services;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin;

[assembly: Dependency(typeof(GeoCodingService))]

namespace XamarinCRM.Services
{
    public class GeoCodingService : IGeoCodingService
    {
        readonly Geocoder _GeoCoder;

        public GeoCodingService()
        {
            _GeoCoder = new Geocoder();
        }

        #region IGeoCodingService implementation

        public Position NullPosition
        {
            get { return new Position(0, 0); }
        }

        public async Task<Position> GeoCodeAddress(string addressString)
        {
            Position p = NullPosition;

            try
            {
                p = (await _GeoCoder.GetPositionsForAddressAsync(addressString)).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: MobileCRM.Shared.Helpers.GeoCodeAddress(): " + ex.Message);
                Insights.Report(ex);
            }

            return p;
        }

        #endregion
    }
}

