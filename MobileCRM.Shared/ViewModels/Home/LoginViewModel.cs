using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        INavigation navigation;

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public const string UsernamePropertyName = "Username";
        string username = string.Empty;

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value, UsernamePropertyName); }
        }

        public const string PasswordPropertyName = "Password";
        string password = string.Empty;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, PasswordPropertyName); }
        }

        Command loginCommand;
        public const string LoginCommandPropertyName = "LoginCommand";

        public Command LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        protected async Task ExecuteLoginCommand()
        {
            await navigation.PopModalAsync();

            Debug.WriteLine(username);
            Debug.WriteLine(password);
        }
    }
}