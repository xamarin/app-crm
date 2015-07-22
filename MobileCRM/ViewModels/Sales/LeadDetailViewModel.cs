using System;
using MobileCRM.ViewModels;
using MobileCRM.Interfaces;
using Xamarin.Forms.Maps;
using MobileCRM.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using MobileCRM.Helpers;
using MobileCRM.Localization;

namespace MobileCRM.ViewModels.Sales
{
    public class LeadDetailViewModel : BaseViewModel
    {
        IDataManager _DataManager;

        Geocoder _Coder;

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

            _DataManager = DependencyService.Get<IDataManager>();

            _Coder = new Geocoder();
        }
        //end ctor

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
            Position p = Utils.NullPosition;
            var address = Lead.AddressString;

            //Lookup Lat/Long all the time.
            //TODO: Only look up if no value, or if address properties have changed.
            //if (Contact.Latitude == 0)
            if (true)
            {
                p = await Utils.GeoCodeAddress(address);
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

            IsBusy = true;

            await _DataManager.SaveAccountAsync(Lead);

            MessagingCenter.Send(Lead, MessagingServiceConstants.SAVE_LEAD);

            IsBusy = false;
        }

        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
    }
}

