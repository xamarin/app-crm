
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