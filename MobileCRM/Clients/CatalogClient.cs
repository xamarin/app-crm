using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileCRM.Clients;
using MobileCRM.Models;

[assembly: Dependency(typeof(CatalogClient))]

namespace MobileCRM.Clients
{
    public class CatalogClient : ICatalogClient
    {
        const string API_SERVICE_URL = "http://xamarindemomobilecrmv2dev.azure-mobile.net/api/";

        const string APP_KEY = "TPNinqLKNLyrHbMfDGKToPddqMPEvr95";

        #region IProductsClient implementation

        public async Task<List<CatalogCategory>> GetCategoriesAsync(string parentCategoryId = null)
        {
            string requestUri = String.Format("Categories/SubCategories?parentCategoryId={0}", parentCategoryId);

            var responseFetcher = new ResponseFetcher<List<CatalogCategory>>(API_SERVICE_URL, APP_KEY);

            return await responseFetcher.GetResponseAsync(requestUri);
        }

        public async Task<CatalogCategory> GetCategoryAsync(string categoryId)
        {
            string requestUri = String.Format("Categories?id={0} ", categoryId);

            var responseFetcher = new ResponseFetcher<CatalogCategory>(API_SERVICE_URL, APP_KEY);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<List<CatalogProduct>> GetProductsAsync(string categoryId)
        {
            string requestUri = String.Format("Products/ByCategory?id={0} ", categoryId);

            var responseFetcher = new ResponseFetcher<List<CatalogProduct>>(API_SERVICE_URL, APP_KEY);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<CatalogProduct> GetProductAsync(string productId)
        {
            string requestUri = String.Format("Products?id={0} ", productId);

            var responseFetcher = new ResponseFetcher<CatalogProduct>(API_SERVICE_URL, APP_KEY);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        public async Task<List<CatalogProduct>> SearchAsync(string searchTerm)
        {
            string requestUri = String.Format("Search?q={0} ", searchTerm);

            var responseFetcher = new ResponseFetcher<List<CatalogProduct>>(API_SERVICE_URL, APP_KEY);

            return await responseFetcher.GetResponseAsync(requestUri).ConfigureAwait(false);
        }

        #endregion
    }
}

