// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinCRM.Extensions;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Models;
using XamarinCRM.Services;

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

        IDataService _DataService;

        public CustomersViewModel()
        {
            this.Title = "Accounts";
            this.Icon = "list.png";

            _DataService = DependencyService.Get<IDataService>();
            Accounts = new ObservableCollection<Account>();

            MessagingCenter.Subscribe<Account>(this, MessagingServiceConstants.ACCOUNT, (account) =>
                {
                    IsInitialized = false;
                });
        }

        Command _LoadAccountsCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadAccountsCommand
        {
            get { return _LoadAccountsCommand ?? (_LoadAccountsCommand = new Command(async () => await ExecuteLoadAccountsCommand())); }
        }

        async Task ExecuteLoadAccountsCommand()
        {
            IsBusy = true;
            LoadAccountsCommand.ChangeCanExecute(); 

            Accounts = (await _DataService.GetAccountsAsync(false)).ToObservableCollection();

            IsBusy = false;
            LoadAccountsCommand.ChangeCanExecute(); 
        }

        Command _LoadAccountsRemoteCommand;

        public Command LoadAccountsRefreshCommand
        {
            get { return _LoadAccountsRemoteCommand ?? (_LoadAccountsRemoteCommand = new Command(async () => await ExecuteLoadAccountsRefreshCommand())); }
        }

        async Task ExecuteLoadAccountsRefreshCommand()
        {
            IsBusy = true;
            LoadAccountsRefreshCommand.ChangeCanExecute(); 

            await _DataService.SynchronizeAccountsAsync();

            Accounts = (await _DataService.GetAccountsAsync(false)).ToObservableCollection();

            IsBusy = false;
            LoadAccountsRefreshCommand.ChangeCanExecute(); 
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