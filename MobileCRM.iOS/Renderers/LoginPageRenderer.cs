using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.iOS.Renderers;
using MobileCRM.Pages.Home;
using MobileCRM.Services;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace MobileCRM.iOS.Renderers
{
    public class LoginPageRenderer : PageRenderer, ILogin
    {
        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            Insights.Track("Login Page");

            await Authenticator.Authenticate(new PlatformParameters(this.ViewController));

            MessagingCenter.Send<ILogin>(this, MessagingServiceConstants.AUTHENTICATED);

//            try
//            {
//                if (AuthInfo.Instance.User == null)
//                {
//                    MobileServiceClient client = AuthInfo.Instance.GetMobileServiceClient();
//
//                    AuthInfo.Instance.User = await client.LoginAsync(this.ViewController, AuthInfo.AUTH_PROVIDER);
//
//                    //SYI: Will implement user info return in v2.
//                    //await AuthInfo.Instance.GetUserInfo();
//
//                    MessagingCenter.Send<ILogin>(this, MessagingServiceConstants.AUTHENTICATED);
//                } //end if
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("ERROR Authenticating: " + ex.Message);
//            } //end catch
        }
    }
    //end class
}