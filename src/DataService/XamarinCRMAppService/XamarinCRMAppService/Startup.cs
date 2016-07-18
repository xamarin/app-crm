

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamarinCRMAppService.Startup))]

namespace XamarinCRMAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}