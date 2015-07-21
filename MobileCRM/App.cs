using MobileCRM.Localization;
using MobileCRM.Pages;
using Xamarin.Forms;

namespace MobileCRM
{
    public class App : Application
    {
        public App()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        static Page _RootPage;

        public static Page RootPage
        {
            get { return _RootPage ?? (_RootPage = new RootPage()); }
        }
    }
}
