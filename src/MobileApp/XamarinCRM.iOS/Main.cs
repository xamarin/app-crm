using UIKit;
using Xamarin;

namespace XamarinCRM.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			Xamarin.Insights.Initialize (XamarinInsights.ApiKey);
			//#if DEBUG
			//Insights.Initialize(Insights.DebugModeKey);
			//#else
			//#endif
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
