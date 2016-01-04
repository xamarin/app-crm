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
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Extensions;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;
using XamarinCRM.Services;

namespace XamarinCRM
{
    public class SalesDashboardLeadsViewModel : BaseViewModel
    {
        IDataService _DataClient;

        Command _LoadSeedDataCommand;

        Command _LoadLeadsCommand;

        ObservableCollection<Account> _Leads;

        public Command PushTabbedLeadPageCommand { get; private set; }

        public bool NeedsRefresh { get; set; }

        public SalesDashboardLeadsViewModel(Command pushTabbedLeadPageCommand, INavigation navigation = null)
            : base(navigation)
        {
            PushTabbedLeadPageCommand = pushTabbedLeadPageCommand;

            _DataClient = DependencyService.Get<IDataService>();

            Leads = new ObservableCollection<Account>();

            MessagingCenter.Subscribe<Account>(this, MessagingServiceConstants.SAVE_ACCOUNT, (account) =>
                {
                    var index = Leads.IndexOf(account);
                    if (index >= 0)
                    {
                        Leads[index] = account;
                    }
                    else
                    {
                        Leads.Add(account);
                    }
                    Leads = new ObservableCollection<Account>(Leads.OrderBy(l => l.Company));
                });

            IsInitialized = false;
        }

        public Command LoadSeedDataCommand
        {
            get
            {
                return _LoadSeedDataCommand ?? (_LoadSeedDataCommand = new Command(async () => await ExecuteLoadSeedDataCommand()));
            }
        }

        /// <summary>
        /// Used for pull-to-refresh of Leads list
        /// </summary>
        /// <value>The load leads command, used for pull-to-refresh.</value>
        public Command LoadLeadsCommand
        { 
            get
            { 
                return _LoadLeadsCommand ?? (_LoadLeadsCommand = new Command(ExecuteLoadLeadsCommand, () => !IsBusy)); 
            } 
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!_DataClient.IsSeeded)
            {
                await _DataClient.SeedLocalDataAsync();
            }

            Leads = (await _DataClient.GetAccountsAsync(true)).ToObservableCollection();

            IsInitialized = true;
            IsBusy = false;
        }

        /// <summary>
        /// Executes the LoadLeadsCommand.
        /// </summary>
        public async void ExecuteLoadLeadsCommand()
        { 
            if (IsBusy)
                return; 

            IsBusy = true;
            LoadLeadsCommand.ChangeCanExecute(); 

            Leads.Clear();
            Leads.AddRange(await _DataClient.GetAccountsAsync(true));

            IsBusy = false;
            LoadLeadsCommand.ChangeCanExecute(); 
        }

        public ObservableCollection<Account> Leads
        {
            get { return _Leads; }
            set
            {
                _Leads = value;
                OnPropertyChanged("Leads");
            }
        }
    }
}

