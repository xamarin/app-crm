
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
                // TODO: log error
            }

            return p;
        }

        #endregion
    }
}

