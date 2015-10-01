using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMv2DataService.Controllers
{
    /// <summary>
    /// Orders API.
    /// </summary>
    public class OrderController : BaseController<Order>
    {
        // GET tables/Order
        public IQueryable<Order> GetAllOrders()
        {
            return Query();
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }

        // Other methods go here if your service is to support CUD operations
    }
}
