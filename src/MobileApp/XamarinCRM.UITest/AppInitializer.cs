using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public static class AppInitializer
    {
        const string apiKey = "YOUR_API_KEY";
        const string apkPath = "../../../XamarinCRM.Android/bin/Release/com.xamarin.xamarincrm-Signed.apk";
        const string appFile = "../../../XamarinCRM.iOS/bin/iPhoneSimulator/Debug/XamarinCRMiOS.app";
        const string bundleId = "com.xamarin.xamarincrm";


        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    //.ApiKey(apiKey)
                    .ApkFile(apkPath)
                    .StartApp();
            }

            return ConfigureApp
                    .iOS
                    //.ApiKey(apiKey)
//                    .AppBundle(appFile)
                    .InstalledApp(bundleId)
                    .StartApp();
        }
    }
}