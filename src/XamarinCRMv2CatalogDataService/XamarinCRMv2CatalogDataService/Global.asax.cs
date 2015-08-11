using System.Web;

namespace XamarinCRMv2CatalogDataService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}