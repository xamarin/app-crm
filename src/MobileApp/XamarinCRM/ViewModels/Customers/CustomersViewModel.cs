using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinCRM.Extensions;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Clients;

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

        ICustomerDataClient dataManager;

        public CustomersViewModel(INavigation navigation = null) : base(navigation)
        {
            this.Title = "Accounts";
            this.Icon = "list.png";

            dataManager = DependencyService.Get<ICustomerDataClient>();
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
            if (IsBusy)
                return;

            IsBusy = true;
            LoadAccountsCommand.ChangeCanExecute(); 

            var accounts = await dataManager.GetAccountsAsync(false);
            Accounts = accounts.ToObservableCollection();

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