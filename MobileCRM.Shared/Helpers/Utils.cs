using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms.Maps;

namespace MobileCRM.Shared.Helpers
{
    public static class Utils
    {

      public static readonly Position NullPosition = new Position(0, 0);
     
      private static Geocoder geoCoder = new Geocoder();

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
              System.Diagnostics.Debug.WriteLine("ERROR: MobileCRM.Shared.Helpers.GeoCodeAddress(): " + exc.Message);
          }
          finally 
          { 
          
          }
          return p;
      }

        
    }
}
