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
                Insights.Report(ex);
            }

            return p;
        }

        #endregion
    }
}

