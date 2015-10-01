using System.Linq;
using System.Web.Http;
using XamarinCRM.Models;

namespace XamarinCRMv2DataService.Controllers
{
    /// <summary>
    /// Accounts API.
    /// </summary>
    public class AccountController : BaseController<Account>
    {
        // GET tables/Account
        public IQueryable<Account> GetAllAccounts()
        {
            return Query();
        }

        // GET tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Account> GetAccount(string id)
        {
            return Lookup(id);
        }

        // Other methods go here if your service is to support CUD operations
    }
}
