using Microsoft.WindowsAzure.MobileServices;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
using MobileCRMAndroid.Renderers;
using MobileCRM;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace MobileCRMAndroid.Renderers
{
    public class LoginPageRenderer : PageRenderer, ILogin
    {
        protected async override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            Insights.Track("Login Page");

            await Authenticator.Authenticate(new PlatformParameters((Activity)this.Context));



//            MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();
//            client.Logout();
//
//            AuthInfo.Instance.User = await client.LoginAsync(this.Context, AuthInfo.AUTH_PROVIDER);
//
//            // Will implement in v2.
//            await AuthInfo.Instance.GetUserInfo();

            MessagingCenter.Send<ILogin>(this, "Authenticated");
        }
    }
}