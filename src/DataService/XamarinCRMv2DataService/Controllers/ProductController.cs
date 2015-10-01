using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMv2DataService.Controllers
{
    /// <summary>
    /// Products API.
    /// </summary>
    public class ProductController : BaseController<Product>
    {
        // GET tables/Product
        public IQueryable<Product> GetAllProducts()
        {
            return Query();
        }

        // GET tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Product> GetProduct(string id)
        {
            return Lookup(id);
        }

        // Other methods go here if your service is to support CUD operations
    }
}