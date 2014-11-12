using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using MobileCRM.Shared.Helpers;

namespace MobileCRM.Shared.ViewModels.Accounts
{
    public class AccountDetailsViewModel : BaseViewModel
    {
      
      IDataManager dataManager;
      Geocoder coder;
      public Account Account { get; set; }

      public AccountDetailsViewModel(Account account)
      {
        if (account == null)
        {
          Account = new Models.Account();
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

        dataManager = DependencyService.Get<IDataManager>();
        coder = new Geocoder();


        MessagingCenter.Subscribe<Account>(this, "Account", (Account) =>
        {
            IsInitialized = false;
        });

      }

      private int industryIndex = 0;
      public int IndustryIndex
      {
          get 
          {
              for (int i = 0; i < Account.IndustryTypes.Length; i++)
              {
                  if (Account.Industry.Equals(Account.IndustryTypes[i]))
                  {
                      industryIndex = i;
                      break;
                  } //end if
              }

              return industryIndex;
          }
          set 
          { 
              industryIndex = value;
              Account.Industry = Account.IndustryTypes[industryIndex]; 
          }
      } //end IndustryIndex


      private int opptStageIndex = 0;
      public int OpptStageIndex
      {
          get
          {
              for (int i = 0; i < Account.OpportunityStages.Length; i++ )
              {
                  if (Account.OpportunityStage.Equals(Account.OpportunityStages[i]))
                  {
                      opptStageIndex = i;
                      break;
                  }
              }
              return opptStageIndex;
          }
          set 
          {
              opptStageIndex = value;
              Account.OpportunityStage = Account.OpportunityStages[opptStageIndex];
          }
      }


      private double dblParsed = 0;
      public string OpportunitySize
      {
          get { return Account.OpportunitySize.ToString(); }
          set 
          {
             
              if (double.TryParse(value, out dblParsed))
              {
                  Account.OpportunitySize = dblParsed;
              }
          }
      }

      
      public string DisplayContact
      {
          get { return Account.DisplayName + ", " + Account.JobTitle; }
      }

      private Command saveAccountCommand;
      /// <summary>
      /// Command to load contacts
      /// </summary>
      public Command SaveAccountCommand
      {
        get
        {
          return saveAccountCommand ??
                 (saveAccountCommand = new Command(async () =>
                  await ExecuteSaveAccountCommand()));
        }
      }

      private async Task ExecuteSaveAccountCommand()
      {
        if (IsBusy)
          return;

        IsBusy = true;


        await dataManager.SaveAccountAsync(Account);

        MessagingCenter.Send(Account, "SaveAccount");

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
              Address = address.ToString()
          };

          return pin;
      }



    }


}
