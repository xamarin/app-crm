using MobileCRM.Localization;
using MobileCRM.Pages;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;

namespace MobileCRM
{
    public class App : Application
    {
        static readonly Lazy<Authenticator> _LazyAuthenticator = new Lazy<Authenticator>(() => new Authenticator());

        static Authenticator _Authenticator { get { return _LazyAuthenticator.Value; } }

        public static Task Authenticate(IPlatformParameters platformParameters)
        {
            return _Authenticator.Authenticate(platformParameters);
        }

        public static Task Logout()
        {
            return _Authenticator.Logout();
        }

        public static bool IsAuthenticated
        {
            get { return _Authenticator.IsAuthenticated; }
        }

        public App()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        
            MainPage = new RootPage();
        }
    }
}
