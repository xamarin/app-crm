using MonoTouch.UIKit;
using Xamarin;

namespace MobileCRM.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c");

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
             UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
