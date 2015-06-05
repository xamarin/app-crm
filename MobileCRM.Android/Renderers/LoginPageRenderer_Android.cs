using Microsoft.WindowsAzure.MobileServices;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MobileCRM.Shared.Pages.Home;
using MobileCRM.Shared.Services;
using MobileCRMAndroid.Renderers;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer_Android))]

namespace MobileCRMAndroid.Renderers
{
    public class LoginPageRenderer_Android : PageRenderer, ILogin
    {
        protected async override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            Insights.Track("Login Page");

            MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();
            client.Logout();

            AuthInfo.Instance.User = await client.LoginAsync(this.Context, AuthInfo.AUTH_PROVIDER);

            //Will implement in v2.
            //await AuthInfo.Instance.GetUserInfo();

            MessagingCenter.Send<ILogin>(this, "Authenticated");
        }
    }
}