
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace XamarinCRM.Services
{
    public interface IGeoCodingService
    {
        Position NullPosition { get; }
        Task<Position> GeoCodeAddress(string addressString);
    }
}

