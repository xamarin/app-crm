using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.WindowsAzure.Mobile.Service;
using XamarinCRMv2CatalogDataService.DataObjects;
using XamarinCRMv2CatalogDataService.Models;

namespace XamarinCRMv2CatalogDataService.Controllers
{
    /// <summary>
    /// Search API endpoints
    /// </summary>
    [RoutePrefix("api/Search")]
    public class SearchController : TableController<Product>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Product>(context, Request, Services);
        }

        // GET tables/Search

        /// <summary>
        /// Returns a collection of products that match the given search term.
        /// </summary>
        /// <param name="q">The search term, which will be matched anywhere in the names, descriptions, and category names of all products in the catalog.</param>
        /// <returns>A collection of products.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable> Search(string q)
        {
            // In an application with a larger data set, you'd want to do something more sophisticated with search, 
            // such as passing the search query to the backing data store, or using something like Elastic Search.
            // The query built here will probably not be very efficient in the backing data store, but with our small data set, it's fine.
            return await 
                Query()
                .Where(x =>
                    x.Name.ToLower().Contains(q.ToLower()) ||
                    x.Description.ToLower().Contains(q.ToLower()) ||
                    x.Category.Name.ToLower().Contains(q.ToLower()))
                .Distinct()
                .ToListAsync();
        }
    }
}