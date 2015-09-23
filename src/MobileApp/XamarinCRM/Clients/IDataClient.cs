using System.Threading.Tasks;
using XamarinCRM.Models;
using System.Collections.Generic;

namespace XamarinCRM.Clients
{
    public interface IDataClient
    {
        Task SeedLocalDataAsync();

        bool LocalDBExists { get; }

        Task SynchronizeAccountsAsync();

        Task SynchronizeOrdersAsync();

        Task SynchronizeCategoriesAsync();

        Task SynchronizeProductsAsync();

        Task SaveOrderAsync(Order item);

        Task DeleteOrderAsync(Order item);

        Task SaveAccountAsync(Account item);

        Task DeleteAccountAsync(Account item);

        Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false);

        Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<IEnumerable<Category>> GetCategoriesAsync(string parentCategoryId = null);

        Task<IEnumerable<Product>> GetProductsAsync(string categoryId);

        Task<IEnumerable<Product>> GetAllChildProductsAsync(string topLevelCategoryId);

        Task<Product> GetProductByNameAsync(string productName);

        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    }
}

