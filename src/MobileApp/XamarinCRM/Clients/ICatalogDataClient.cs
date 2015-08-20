using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinCRM.Models;

namespace XamarinCRM.Clients
{
    public interface ICatalogDataClient
    {
        Task<List<CatalogCategory>> GetCategoriesAsync(string parentCategoryId = null);

        Task<CatalogCategory> GetCategoryAsync(string categoryId);

        Task<List<CatalogProduct>> GetProductsAsync(string categoryId);

        Task<List<CatalogProduct>> GetAllChildProductsAsync(string topLevelCategoryId);

        Task<CatalogProduct> GetProductByIdAsync(string productId);

        Task<CatalogProduct> GetProductByNameAsync(string productName);

        Task<List<CatalogProduct>> SearchAsync(string searchTerm);
    }
}

