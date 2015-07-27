using Microsoft.WindowsAzure.MobileServices;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
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

<<<<<<< Updated upstream:MobileCRM.Android/Renderers/LoginPageRenderer_Android.cs
            MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();
            client.Logout();
=======
//            await Authenticator.Authenticate(new PlatformParameters((Activity)this.Context));
>>>>>>> Stashed changes:MobileCRM.Android/Renderers/LoginPageRenderer.cs

            AuthInfo.Instance.User = await client.LoginAsync(this.Context, AuthInfo.AUTH_PROVIDER);

            //Will implement in v2.
            //await AuthInfo.Instance.GetUserInfo();

            MessagingCenter.Send<ILogin>(this, "Authenticated");
        }
    }
}