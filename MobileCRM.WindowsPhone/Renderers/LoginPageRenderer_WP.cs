using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

using MobileCRM.Shared.Pages.Home;
using MobileCRM.Shared.Services;

using MobileCRM.WindowsPhone.Renderers;

[assembly: ExportRenderer(typeof(MobileCRM.Shared.Pages.Home.LoginPage), typeof(LoginPageRenderer_WP))]

namespace MobileCRM.WindowsPhone.Renderers
{
    public class LoginPageRenderer_WP : PageRenderer, ILogin
    {

        protected async override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();

            AuthInfo.Instance.User = await client.LoginAsync(AuthInfo.AUTH_PROVIDER);

            //Will implement in v2.
            //await AuthInfo.Instance.GetUserInfo();

            MessagingCenter.Send<ILogin>(this, "Authenticated");
        }

    }

}
