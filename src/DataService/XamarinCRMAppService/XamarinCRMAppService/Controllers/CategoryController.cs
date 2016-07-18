

using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using XamarinCRM.Models;
using XamarinCRMAppService.Models;

namespace XamarinCRMAppService.Controllers
{
    public class CategoryController : BaseController<Category>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Category>(context, Request);
        }

        // GET tables/Category
        public IQueryable<Category> GetAllCategory()
        {
            return Query(); 
        }

        // GET tables/Category/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Category> GetCategory(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Category/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent PATCH operations
        //public Task<Category> PatchCategory(string id, Delta<Category> patch)
        //{
        //     return UpdateAsync(id, patch);
        //}

        // POST tables/Category
        // Omitted intentionally to prevent POST operations
        //public async Task<IHttpActionResult> PostCategory(Category item)
        //{
        //    Category current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Category/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent DELETE operations
        //public Task DeleteCategory(string id)
        //{
        //     return DeleteAsync(id);
        //}
    }
}
