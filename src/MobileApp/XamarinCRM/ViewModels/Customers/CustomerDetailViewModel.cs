using System.Threading.Tasks;
using XamarinCRM.Helpers;
using XamarinCRM.Interfaces;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinCRM.ViewModels.Customers
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        IDataManager _DataManager;
        Geocoder _GeoCoder;

        public Account Account { get; set; }

        public CustomerDetailViewModel(Account account)
        {
            if (account == null)
            {
                Account = new Account();
                Account.Industry = Account.IndustryTypes[0];
                Account.OpportunityStage = Account.OpportunityStages[0];

                this.Title = "New Account";
            }
            else
            {
                Account = account;
                this.Title = "Account";
            }

            this.Icon = "account.png";

            _DataManager = DependencyService.Get<IDataManager>();
            _GeoCoder = new Geocoder();

            MessagingCenter.Subscribe<Account>(this, MessagingServiceConstants.ACCOUNT, (Account) =>
                {
                    IsInitialized = false;
                });
        }

        int _IndustryIndex = 0;

        public int IndustryIndex
        {
            get
            {
                for (int i = 0; i < Account.IndustryTypes.Length; i++)
                {
                    if (Account.Industry.Equals(Account.IndustryTypes[i]))
                    {
                        _IndustryIndex = i;
                        break;
                    } //end if
                }

                return _IndustryIndex;
            }
            set
            { 
                _IndustryIndex = value;
                Account.Industry = Account.IndustryTypes[_IndustryIndex]; 
            }
        }
        //end IndustryIndex

        int _OpptStageIndex = 0;

        public int OpptStageIndex
        {
            get
            {
                for (int i = 0; i < Account.OpportunityStages.Length; i++)
                {
                    if (Account.OpportunityStage.Equals(Account.OpportunityStages[i]))
                    {
                        _OpptStageIndex = i;
                        break;
                    }
                }
                return _OpptStageIndex;
            }
            set
            {
                _OpptStageIndex = value;
                Account.OpportunityStage = Account.OpportunityStages[_OpptStageIndex];
            }
        }

        double _DblParsed = 0;

        public string OpportunitySize
        {
            get { return Account.OpportunitySize.ToString(); }
            set
            {
             
                if (double.TryParse(value, out _DblParsed))
                {
                    Account.OpportunitySize = _DblParsed;
                }
            }
        }
      
        public string DisplayContact
        {
            get { return Account.DisplayName + ", " + Account.JobTitle; }
        }

        Command _SaveAccountCommand;

        /// <summary>
        /// Command to load contacts
        /// </summary>
        public Command SaveAccountCommand
        {
            get
            {
                return _SaveAccountCommand ??
                (_SaveAccountCommand = new Command(async () =>
                  await ExecuteSaveAccountCommand()));
            }
        }

        async Task ExecuteSaveAccountCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;


            await _DataManager.SaveAccountAsync(Account);

            MessagingCenter.Send(Account, MessagingServiceConstants.SAVE_ACCOUNT);

            IsBusy = false;

            Navigation.PopAsync();
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task<Pin> LoadPin()
        {
            Position p = Utils.NullPosition;
            var address = Account.AddressString;

            //Lookup Lat/Long all the time unless an account where the address is read-only
            //TODO: Only look up if no value, or if address properties have changed.
            //if (Contact.Latitude == 0)
            if (Account.IsLead)
            {
                p = await Utils.GeoCodeAddress(address);

                Account.Latitude = p.Latitude;
                Account.Longitude = p.Longitude;
            }
            else
            {
                p = new Position(Account.Latitude, Account.Longitude);
            }

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = p,
                Label = Account.Company,
                Address = address
            };

            return pin;
        }
    }
}