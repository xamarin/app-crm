using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
using MobileCRM.WindowsPhone.Renderers;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using LoginPage = MobileCRM.Pages.Home.LoginPage;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer_WP))]

namespace MobileCRM.WindowsPhone.Renderers
{
    public class LoginPageRenderer_WP : PageRenderer, ILogin
    {

        protected async override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            Insights.Track("Login Page");

            MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();

            AuthInfo.Instance.User = await client.LoginAsync(AuthInfo.AUTH_PROVIDER);

            //Will implement in v2.
            //await AuthInfo.Instance.GetUserInfo();

            MessagingCenter.Send<ILogin>(this, MessagingServiceConstants.AUTHENTICATED);
        }

    }

}
