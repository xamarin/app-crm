using UIKit;
using Xamarin;

namespace XamarinCRM.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // Initialize insights
            Insights.Initialize("2b82ddc37582e6c1bece7e5901e8bae3bf7bfb26");

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
             UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
