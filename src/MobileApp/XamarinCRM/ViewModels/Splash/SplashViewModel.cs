//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

