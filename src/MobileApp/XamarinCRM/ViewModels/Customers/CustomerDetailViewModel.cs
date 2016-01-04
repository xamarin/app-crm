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
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Models;
using XamarinCRM.Pages.Customers;
using XamarinCRM.Services;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM.ViewModels.Customers
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        IDataService _DataClient;

        readonly IGeoCodingService _GeoCodingService;

        public Account Account { get; set; }

        readonly Page _CurrentPage;

        public Page CurrentPage
        {
            get { return _CurrentPage; }
        }

        public CustomerDetailViewModel(Account account, Page currentPage)
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

            _CurrentPage = currentPage;

            this.Icon = "account.png";

            _DataClient = DependencyService.Get<IDataService>();
            _GeoCodingService = DependencyService.Get<IGeoCodingService>();

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
                    }
                }

                return _IndustryIndex;
            }
            set
            { 
                _IndustryIndex = value;
                Account.Industry = Account.IndustryTypes[_IndustryIndex]; 
            }
        }

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

        ICommand _SaveAccountCommand;

        public ICommand SaveAccountCommand
        {
            get
            {
                return _SaveAccountCommand ??
                (_SaveAccountCommand = new Command(async () =>
                    {
                        if (IsBusy)
                            return;


                        IsBusy = true;


                        await _DataClient.SaveAccountAsync(Account);

                        MessagingCenter.Send(Account, MessagingServiceConstants.SAVE_ACCOUNT);

                        IsBusy = false;

                        await Navigation.PopAsync();
                    }));
            }
        }

        ICommand _PhoneIconTappedCommand;

        public ICommand PhoneIconTappedCommand
        {
            get
            {  
                return _PhoneIconTappedCommand ?? (_PhoneIconTappedCommand = new Command<Label>(async label => await ExecutePhoneIconTappedCommand(label)));
            }
        }

        async Task ExecutePhoneIconTappedCommand(Label label)
        {
            if (label == null)
                return;

            string phoneNumber = label.Text;

            if (String.IsNullOrWhiteSpace(phoneNumber))
                return;        

            if (await CurrentPage.DisplayAlert(
                    title: TextResources.Customers_Detail_CallDialog_Title,
                    message: TextResources.Customers_Detail_CallDialog_Message + phoneNumber + "?",
                    accept: TextResources.Customers_Detail_CallDialog_Accept,
                    cancel: TextResources.Customers_Detail_CallDialog_Cancel))
            {
                var phoneCallTask = MessagingPlugin.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                    phoneCallTask.MakePhoneCall(phoneNumber.Replace("-", ""));
            }
        }

        ICommand _MapMarkerIconTappedCommand;

        public ICommand MapMarkerIconTappedCommand
        {
            get
            {  
                return _MapMarkerIconTappedCommand ??
                    (_MapMarkerIconTappedCommand = new Command(async () => await ExecuteMapMarkerIconTappedCommand()));
            }
        }

        async Task ExecuteMapMarkerIconTappedCommand()
        {
            await PushAsync(new CustomerMapPage()
                {
                    Title = Account.Company,
                    BindingContext = this
                });
        }


        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task<Pin> LoadPin()
        {
            Position p = _GeoCodingService.NullPosition;

            var address = Account.AddressString;

            //Lookup Lat/Long all the time unless an account where the address is read-only
            //TODO: Only look up if no value, or if address properties have changed.
            //if (Contact.Latitude == 0)
            if (Account.IsLead)
            {
                p = await _GeoCodingService.GeoCodeAddress(address);

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