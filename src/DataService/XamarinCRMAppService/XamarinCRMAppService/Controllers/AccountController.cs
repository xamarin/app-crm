

using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMAppService.Controllers
{
    public class AccountController : BaseController<Account>
    {
        // GET tables/Account
        public IQueryable<Account> GetAllAccount()
        {
            return Query(); 
        }

        // GET tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Account> GetAccount(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent PATCH operations
        //public Task<Account> PatchAccount(string id, Delta<Account> patch)
        //{
        //     return UpdateAsync(id, patch);
        //}

        // POST tables/Account
        // Omitted intentionally to prevent POST operations
        //public async Task<IHttpActionResult> PostAccount(Account item)
        //{
        //    Account current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        // DELETE tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        // Omitted intentionally to prevent DELETE operations
        //public Task DeleteAccount(string id)
        //{
        //     return DeleteAsync(id);
        //}
    }
}
