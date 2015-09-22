using System.Linq;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using XamarinCRMv2DataService.DataObjects;

namespace XamarinCRMv2DataService.Controllers
{
    public class ProductController : TableController<Product>
    {
        // GET tables/Category
        public IQueryable<Product> GetAllProducts()
        {
            return Query();
        }

        // GET tables/Category/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Product> GetProduct(string id)
        {
            return Lookup(id);
        }
    }
}