
using System;
using System.Threading.Tasks;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Services;
using XamarinCRM.Models;

namespace XamarinCRM.ViewModels.Sales
{
    public class LeadDetailViewModel : BaseViewModel
    {
        readonly IDataService _DataClient;

        IGeoCodingService _GeoCodingService;

        public Account Lead { get; set; }

        public LeadDetailViewModel(INavigation navigation, Account lead = null)
        {
            if (navigation == null)
            {
                throw new ArgumentNullException("navigation", "An instance of INavigation must be passed to the LeadDetailViewModel constructor.");
            }

            Navigation = navigation;

            if (lead == null)
            {
                Lead = new Account();
                this.Title = TextResources.Leads_NewLead;
            }
            else
            {
                Lead = lead;
                this.Title = lead.Company;
            }

            this.Icon = "contact.png";

            _DataClient = DependencyService.Get<IDataService>();

            _GeoCodingService = DependencyService.Get<IGeoCodingService>();
        }

        Command saveLeadCommand;

        /// <summary>
        /// Command to load contacts
        /// </summary>
        public Command SaveLeadCommand
        {
            get
            {
                return saveLeadCommand ??
                (saveLeadCommand = new Command(async () =>
                        await ExecuteSaveLeadCommand()));
            }
        }

        public async Task<Pin> LoadPin()
        {
            Position p = _GeoCodingService.NullPosition;
            var address = Lead.AddressString;

            //Lookup Lat/Long all the time.
            //TODO: Only look up if no value, or if address properties have changed.
            //if (Contact.Latitude == 0)
            if (true)
            {
                p = await _GeoCodingService.GeoCodeAddress(address);
                //p = p == null ? Utils.NullPosition : p;

                Lead.Latitude = p.Latitude;
                Lead.Longitude = p.Longitude;
            }

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = p,
                Label = Lead.DisplayName,
                Address = address
            };

            return pin;
        }

        async Task ExecuteSaveLeadCommand()
        {
            if (IsBusy)
                return;


            if (string.IsNullOrWhiteSpace(Lead.Company))
            {

                MessagingCenter.Send(Lead, MessagingServiceConstants.SAVE_LEAD_ERROR);
                return;
            }

            IsBusy = true;

            await _DataClient.SaveAccountAsync(Lead);

            MessagingCenter.Send(Lead, MessagingServiceConstants.SAVE_LEAD);

            IsBusy = false;
        }
    }
}

