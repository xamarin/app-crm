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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinCRM.Extensions;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Clients;
using XamarinCRM.Models;

namespace XamarinCRM.ViewModels.Customers
{
    public class CustomersViewModel : BaseViewModel
    {
        ObservableCollection<Account> _Accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return _Accounts; }
            set
            {
                _Accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        IDataClient _DataClient;

        public CustomersViewModel(INavigation navigation = null) : base(navigation)
        {
            this.Title = "Accounts";
            this.Icon = "list.png";

            _DataClient = DependencyService.Get<IDataClient>();
            Accounts = new ObservableCollection<Account>();

            MessagingCenter.Subscribe<Account>(this, MessagingServiceConstants.ACCOUNT, (account) =>
                {
                    IsInitialized = false;
                });
        }

        Command loadAccountsCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadAccountsCommand
        {
            get
            {
                return loadAccountsCommand ??
                (loadAccountsCommand = new Command(async () =>
                  await ExecuteLoadAccountsCommand()));
            }
        }

        async Task ExecuteLoadAccountsCommand()
        {
            IsBusy = true;
            LoadAccountsCommand.ChangeCanExecute(); 

            Accounts = (await _DataClient.GetAccountsAsync(false)).ToObservableCollection();

            IsBusy = false;
            LoadAccountsCommand.ChangeCanExecute(); 
        }

        public static readonly Position NullPosition = new Position(0, 0);

        public List<Pin> LoadPins()
        {
            var pins = Accounts.Select(model =>
                {
                    var item = model;
                    var address = item.AddressString;

                    var position = address != null ? new Position(item.Latitude, item.Longitude) : NullPosition;
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = item.ToString(),
                        Address = address?.ToString()
                    };
                    return pin;
                }).ToList();

            return pins; 
        }
    }
}