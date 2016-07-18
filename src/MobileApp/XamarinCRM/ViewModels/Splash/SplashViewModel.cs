
using System.Threading.Tasks;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinCRM.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        readonly IConfigFetcher _ConfigFetcher;

        public SplashViewModel(INavigation navigation = null)
            : base(navigation)
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();
        }

        bool _IsPresentingLoginUI;

        public bool IsPresentingLoginUI
        {
            get
            { 
                return _IsPresentingLoginUI;
            }
            set
            { 
                _IsPresentingLoginUI = value;
                OnPropertyChanged("IsPresentingLoginUI");
            }
        }

        string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged("Username");
            }
        }

        string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }

        public async Task LoadDemoCredentials()
        {
            Username = await _ConfigFetcher.GetAsync("azureActiveDirectoryUsername", true);
            Password = await _ConfigFetcher.GetAsync("azureActiveDirectoryPassword", true);
        }
    }
}

