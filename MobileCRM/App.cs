using MobileCRM.Pages.Home;
using Xamarin.Forms;

namespace MobileCRM
{
    public class App : Application
    {
        static Page _HomeView;

        public static Page RootPage
        {
            get { return _HomeView ?? (_HomeView = new RootPage()); }
        }
    }
}
