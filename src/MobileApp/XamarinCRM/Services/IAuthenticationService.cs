using System.Threading.Tasks;

namespace XamarinCRM.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate();

        Task<bool> Logout();

        bool IsAuthenticated { get; }
    }
}

