

using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMAppService.Controllers
{
    public class ProductController : BaseController<Product>
    {
        // GET tables/Product
        public IQueryable<Product> GetAllProduct()
        {
            return Query(); 
        }

        // GET tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Product> GetProduct(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent PATCH operations
        //public Task<Product> PatchProduct(string id, Delta<Product> patch)
        //{
        //     return UpdateAsync(id, patch);
        //}

        // POST tables/Product
        // Omitted intentionally to prevent POST operations
        //public async Task<IHttpActionResult> PostProduct(Product item)
        //{
        //    Product current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent DELETE operations
        //public Task DeleteProduct(string id)
        //{
        //     return DeleteAsync(id);
        //}
    }
}
