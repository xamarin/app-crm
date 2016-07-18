

using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using XamarinCRM.Models;
using XamarinCRMAppService.Models;

namespace XamarinCRMAppService.Controllers
{
    [MobileAppController]
    public abstract class BaseController<T> : TableController<T> where T : BaseModel
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<T>(context, Request);
        }
    }
}
