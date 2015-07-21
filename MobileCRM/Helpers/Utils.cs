using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MobileCRM.Helpers
{
    public static class Utils
    {
        public static readonly Position NullPosition = new Position(0, 0);
     
        static Geocoder geoCoder = new Geocoder();

        public async static Task<Position> GeoCodeAddress(string addressString)
        {
            Position p = NullPosition;

            try
            {
                Task<IEnumerable<Position>> getPosTask = geoCoder.GetPositionsForAddressAsync(addressString);
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
    }
}
