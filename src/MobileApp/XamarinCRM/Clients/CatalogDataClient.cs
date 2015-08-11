using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using Xamarin.Forms;
using XamarinCRM.Services;

[assembly: Dependency(typeof(CatalogDataClient))]

namespace XamarinCRM.Clients
{
    public class CatalogDataClient : ICatalogDataClient
    {
        readonly string _ApiServiceUrl;

        readonly string _ApiAppKey;

        readonly IConfigFetcher _ConfigFetcher;

        public CatalogDataClient()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _ApiServiceUrl = _ConfigFetcher.GetAsync("catalogDataServiceUrl").Result;
            _ApiAppKey = _ConfigFetcher.GetAsync("catalogDataServiceAppKey", true).Result;
        }

        #region IProductsClient implementation

        public async Task<List<CatalogCategory>> GetCategoriesAsync(string parentCategoryId = null)
        {
            string requestUri = String.Format("Categories/SubCategories?parentCategoryId={0}", parentCategoryId);

            var responseFetcher = new ResponseFetcher<List<CatalogCategory>>(_ApiServiceUrl, _ApiAppKey);

            return await responseFetcher.GetResponseAsync(requestUri);
        }

        public async Task<CatalogCategory> GetCategoryAsync(string categoryId)
        {
            string requestUri = String.Format("Categories?id={0} ", categoryId);

            var responseFetcher = new ResponseFetcher<CatalogCategory>(_ApiServiceUrl, _ApiAppKey);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<List<CatalogProduct>> GetProductsAsync(string categoryId)
        {
            string requestUri = String.Format("Products/ByCategory?id={0} ", categoryId);

            var responseFetcher = new ResponseFetcher<List<CatalogProduct>>(_ApiServiceUrl, _ApiAppKey);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<CatalogProduct> GetProductAsync(string productId)
        {
            string requestUri = String.Format("Products?id={0} ", productId);

            var responseFetcher = new ResponseFetcher<CatalogProduct>(_ApiServiceUrl, _ApiAppKey);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<List<CatalogProduct>> SearchAsync(string searchTerm)
        {
            string requestUri = String.Format("Search?q={0} ", searchTerm);

            var responseFetcher = new ResponseFetcher<List<CatalogProduct>>(_ApiServiceUrl, _ApiAppKey);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        #endregion
    }
}

