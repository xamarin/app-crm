using System.Linq;
using System.Web.Http;
using XamarinCRMv2DataService.DataObjects;

namespace XamarinCRMv2DataService.Controllers
{
    /// <summary>
    /// Categories API.
    /// </summary>
    public class CategoryController : BaseController<Category>
    {
        // GET tables/Category
        public IQueryable<Category> GetAllCategories()
        {
            return Query();
        }

        // GET tables/Category/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Category> GetCategory(string id)
        {
            return Lookup(id);
        }

        // Other methods go here if your service is to support CUD operations
    }
}