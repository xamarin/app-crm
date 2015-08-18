using System;
using XamarinCRM.Services;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

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
                Task<IEnumerable<Position>> getPosTask = _GeoCoder.GetPositionsForAddressAsync(addressString);
                IEnumerable<Position> pos = await getPosTask;
                p = pos == null ? p : pos.First();

            }
            catch (Exception exc)
            {
                Debug.WriteLine("ERROR: MobileCRM.Shared.Helpers.GeoCodeAddress(): " + exc.Message);
            }
            finally
            { 

            }
            return p;
        }

        #endregion
    }
}

