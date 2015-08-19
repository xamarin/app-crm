using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinCRM.Models;

namespace XamarinCRM.Clients
{
    public interface ICustomerDataClient
    {
        Task SeedDataAsync();

        bool DoesLocalDBExist();

        Task SynchronizeAccountsAsync();

        Task SynchronizeOrdersAsync();

        Task SaveOrderAsync(Order item);

        Task DeleteOrderAsync(Order item);

        Task SaveAccountAsync(Account item);

        Task DeleteAccountAsync(Account item);

        Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false);

        Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
