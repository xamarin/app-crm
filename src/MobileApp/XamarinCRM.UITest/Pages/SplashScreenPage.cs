using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class SplashScreenPage : BasePage
    {
        readonly string EnterSignInButton = "SIGN IN";

        public SplashScreenPage(IApp app, Platform platform)
            : base(app, platform, "SIGN IN", "SIGN IN")
        {
        }

        public void ExitSplashScreen()
        {
            app.Tap(EnterSignInButton);
        }
    }
}

