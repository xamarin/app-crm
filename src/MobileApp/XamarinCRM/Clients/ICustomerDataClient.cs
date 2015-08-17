using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinCRM.Models;

namespace XamarinCRM.Clients
{
    public interface ICustomerDataClient
    {
        Task SeedData();

        bool DoesLocalDBExist();

        Task SyncAccounts();

        Task SyncOrders();

        Task SaveOrderAsync(Order item);

        Task DeleteOrderAsync(Order item);

        Task SaveAccountAsync(Account item);

        Task DeleteAccountAsync(Account item);

        Task<IEnumerable<Account>> GetAccountsAsync(bool leads);

        //Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId, bool open);

        Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId);

        Task<IEnumerable<Order>> GetAccountOrderHistoryAsync(string accountId);

        Task<IEnumerable<Order>> GetAllAccountOrdersAsync();
    }
}
