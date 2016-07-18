

using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMAppService.Controllers
{
    public class OrderController : BaseController<Order>
    {
        // GET tables/Order
        public IQueryable<Order> GetAllOrder()
        {
            return Query();
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent PATCH operations
        //public Task<Order> PatchOrder(string id, Delta<Order> patch)
        //{
        //     return UpdateAsync(id, patch);
        //}

        // POST tables/Order
        // Omitted intentionally to prevent POST operations
        //public async Task<IHttpActionResult> PostOrder(Order item)
        //{
        //    Order current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent DELETE operations
        //public Task DeleteOrder(string id)
        //{
        //     return DeleteAsync(id);
        //}
    }
}
