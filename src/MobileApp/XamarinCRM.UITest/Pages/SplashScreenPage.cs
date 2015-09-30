using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class SplashScreenPage : BasePage
    {
        readonly string EnterSignInButton = "SIGN IN (AZURE AD)";

        public SplashScreenPage(IApp app, Platform platform)
            : base(app, platform, "SIGN IN (AZURE AD)", "SIGN IN (AZURE AD)")
        {
        }

        public void ExitSplashScreen()
        {
            app.Tap(EnterSignInButton);
        }
    }
}

