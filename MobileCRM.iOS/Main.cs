using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;


namespace MobileCRM.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main (string[] args)
        {
          //var platform = SQLite3Provider.Instance;


            Insights.Initialize("5f085f0e12b1bf6b15592ad059354f7e416571e4");


            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "AppDelegate");
        }
    }
}
