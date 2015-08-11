using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
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
            return await 
                Query()
                .Where(x => x.CategoryId == id)
                .ToListAsync();
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
            return await
                Query()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}