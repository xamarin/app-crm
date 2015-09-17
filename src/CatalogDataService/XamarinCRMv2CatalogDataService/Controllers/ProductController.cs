using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Microsoft.Data.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using XamarinCRMv2CatalogDataService.DataObjects;

namespace XamarinCRMv2CatalogDataService.Controllers
{
    /// <summary>
    /// Product API endpoints
    /// </summary>
    [RoutePrefix("api/Products")]
    public class ProductController : BaseController<Product>
    {
        // GET api/Products/ByCategory/48D68C86-6EA6-4C25-AA33-223FC9A27959

        /// <summary>
        /// Gets a collection of products of a given category.
        /// </summary>
        /// <param name="id">The id of the category for which to retrieve products.</param>
        /// <returns>A collection of products.</returns>
        [Route("ByCategory")]
        public async Task<IEnumerable<Product>> GetProductsByCategory(string id)
        {
            var products = await 
                Query()
                .Where(x => x.CategoryId.Trim().ToLower() == id.Trim().ToLower())
                .ToListAsync();

            return products;
        }

        // GET api/Products/48D68C86-6EA6-4C25-AA33-223FC9A27959

        /// <summary>
        /// Gets a specific product by id.
        /// </summary>
        /// <param name="id">The id of the product to retrieve.</param>
        /// <returns>A product.</returns>
        [Route("")]
        public async Task<Product> GetProduct(string id)
        {
            var product = await
                Query()
                .SingleOrDefaultAsync(x => x.Id.Trim().ToLower() == id.Trim().ToLower());

            return product;
        }

        /// <summary>
        /// Gets a product by its name, case insensitive.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("ByName")]
        public async Task<Product> GetProductByName(string name)
        {
            var product =  await
                Query()
                    .SingleOrDefaultAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower());

            return product;
        }

        /// <summary>
        /// Gets all descendant products of a given top level category.
        /// </summary>
        /// <param name="id">The id of the top level category for which to retrieve products.</param>
        /// <returns>A collection of products.</returns>
        [Route("ByTopLevelCategory")]
        public async Task<IEnumerable<Product>> GetAllChildProductsOfTopLevelCategory(string id)
        {
            EntityDomainManager<Category> categoryDomainManager = new EntityDomainManager<Category>(MobileServiceContext, Request, Services);

            var category = categoryDomainManager.Query().SingleOrDefault(x => x.Id.Trim().ToLower() == id.Trim().ToLower());

            if (category == null)
            {
                Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError() { Message = string.Format("There is no category with id {0}", id)});
            }

            if (category.ParentCategory.Id != null)
            {
                Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError() { Message = string.Format("Category Id {0} must be for a root level category", id) });
            }


            var leafLevelCategories = await GetLeafLevelCategories(id);

            List<Product> products = new List<Product>();

            foreach (var c in leafLevelCategories)
            {
                products.AddRange(await GetProductsByCategory(c.Id));
            }

            return products;
        }

        private async Task<IEnumerable<Category>> GetLeafLevelCategories(string id)
        {
            EntityDomainManager<Category> categoryDomainManager = new EntityDomainManager<Category>(MobileServiceContext, Request, Services);

            List<Category> categories = new List<Category>();

            Category category = categoryDomainManager.Query().Single(x => x.Id.Trim().ToLower() == id.Trim().ToLower());

            if (category.HasSubCategories)
            {
                foreach (var c in category.SubCategories)
                {
                    categories.AddRange(await GetLeafLevelCategories(c.Id));
                }
            }
            else
            {
                categories.Add(category);
            }

            return categories;
        }
    }
}