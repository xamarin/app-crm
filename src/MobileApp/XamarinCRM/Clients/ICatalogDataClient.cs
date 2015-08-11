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

        Task<CatalogProduct> GetProductAsync(string productId);

        Task<List<CatalogProduct>> SearchAsync(string searchTerm);
    }
}

