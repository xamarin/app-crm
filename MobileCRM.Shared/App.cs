using MobileCRM.Shared.Pages.Home;
using Xamarin.Forms;

namespace MobileCRM.Shared
{
    public static class App
    {
        static Page _HomeView;

        public static Page RootPage
        {
            get { return _HomeView ?? (_HomeView = new RootPage()); }
        }
    }
}
