using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Extensions;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM
{
    public class SalesDashboardLeadsViewModel : BaseViewModel
    {
        ICustomerDataClient _CustomerDataClient;

        Command _LoadSeedDataCommand;

        Command _LoadLeadsCommand;

        ObservableCollection<Account> _Leads;

        readonly Command _PushTabbedLeadPageCommand;

        public Command PushLeadDetailsTabbedPageCommand
        { 
            get { return _PushTabbedLeadPageCommand; } 
        }

        public bool NeedsRefresh { get; set; }

        public SalesDashboardLeadsViewModel(Command pushTabbedLeadPageCommand, INavigation navigation = null)
            : base(navigation)
        {
            _PushTabbedLeadPageCommand = pushTabbedLeadPageCommand;

            _CustomerDataClient = DependencyService.Get<ICustomerDataClient>();

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

            await _CustomerDataClient.SeedDataAsync();

            var leads = await _CustomerDataClient.GetAccountsAsync(true);
            Leads = leads.ToObservableCollection();

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
            Leads.AddRange(await _CustomerDataClient.GetAccountsAsync(true));

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

