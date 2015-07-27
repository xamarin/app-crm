using MobileCRM.Pages.Home;
using Xamarin.Forms;
<<<<<<< Updated upstream
=======
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;
>>>>>>> Stashed changes

namespace MobileCRM
{
    public class App : Application
    {
<<<<<<< Updated upstream
        static Page _HomeView;
=======
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
>>>>>>> Stashed changes

        public static bool IsAuthenticated
        {
<<<<<<< Updated upstream
            get { return _HomeView ?? (_HomeView = new RootPage()); }
=======
            get { return _Authenticator.IsAuthenticated; }
        }

        public App()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        
            MainPage = new RootPage();
>>>>>>> Stashed changes
        }
    }
}
