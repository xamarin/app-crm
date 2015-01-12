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


            Insights.Initialize("f10683d3b439eb285201591cf4c9f4577e95fb08");


            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "AppDelegate");
        }
    }
}
