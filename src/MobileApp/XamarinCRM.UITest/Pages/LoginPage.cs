using System.Linq;
using System.Threading;
using Xamarin.UITest;
using WebQuery = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppWebQuery>;

namespace XamarinCRM.UITest
{
    public class LoginPage : BasePage
    {
        readonly WebQuery EmailField = x => x.Css("#cred_userid_inputtext");
        readonly WebQuery PasswordField = x => x.Css("#cred_password_inputtext");
        readonly WebQuery SignInButton = x => x.Css("#cred_sign_in_button");
        readonly WebQuery WorkAccountButton = x => x.Css("#aad_account_tile");
        readonly WebQuery PersonalAccount = x => x.Css("#mso_account_tile");
        readonly WebQuery PersonalPassword = x => x.Css("#i0118");
        readonly WebQuery PersonalSignIn = x => x.Css("#idSIButton9");


        public LoginPage(IApp app, Platform platform)
            : base(app, platform, "Sign in", "Sign in")
        {
        }

        public void LogInWithPersonalCredentials(string email, string password)
        {
            app.EnterText(EmailField, email);
            app.Screenshot("Email entered");
            app.PressEnter();
            app.DismissKeyboard();
            app.WaitForElement(PersonalAccount);
            app.Screenshot("Account type choser appears. Choosing personal account.");
            app.Tap(PersonalAccount);

            app.WaitForElement(PersonalPassword);
            app.Screenshot("Redirected");
            app.EnterText(PersonalPassword, password);
            app.DismissKeyboard();
            app.Screenshot("Password entered");

            app.WaitForElement(PersonalSignIn);
            app.Screenshot("Signing in");
            app.DismissKeyboard();
            app.Tap(PersonalSignIn);
        }

        public void LogInWithWorkCredentials(string email, string password)
        {
            app.EnterText(EmailField, email);
            app.Screenshot("Email entered");
            app.PressEnter();
            app.DismissKeyboard();
//            app.WaitForElement(workAccountButton);
//            app.Screenshot("Account type choser appears. Choosing work account.");
//            app.Tap(workAccountButton);

            app.WaitForElement(PasswordField);
            app.EnterText(PasswordField, password);
            app.DismissKeyboard();
            app.Screenshot("Password entered");

            app.WaitForElement(SignInButton);
            app.Screenshot("Signing in");

            app.Tap(EmailField);
            //Keyboard has to animate up to animate away...
            if (app.Query(SignInButton).Any())
                Thread.Sleep(3000);
                
            app.DismissKeyboard();
            app.Tap(SignInButton);
        }

    }
}

