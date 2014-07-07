using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
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
      INavigation navigation;
      Geocoder coder;
      public Account Account { get; set; }

      public AccountDetailsViewModel(INavigation navigation, Account account)
      {
        if (account == null)
        {
          Account = new Models.Account();
          this.Title = "New Account";
        }
        else
        {
          Account = account;
          this.Title = "Edit Account";
        }
        
        dataManager = DependencyService.Get<IDataManager>();
        this.navigation = navigation;
        coder = new Geocoder();
      }


      private Command saveContactCommand;
      /// <summary>
      /// Command to load contacts
      /// </summary>
      public Command SaveContactCommand
      {
        get
        {
          return saveContactCommand ??
                 (saveContactCommand = new Command(async () =>
                  await ExecuteSaveContactCommand()));
        }
      }

      private async Task ExecuteSaveContactCommand()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        IEnumerable<Position> points = await coder.GetPositionsForAddressAsync(Account.AddressString);
        if(points != null && points.Count() > 0)
        {
          var point = points.ElementAt(0);
          Account.Latitude = point.Latitude;
          Account.Longitude = point.Longitude;
        }
        await dataManager.SaveAccountAsync(Account);

        MessagingCenter.Send(Account, "NewAccount");

        IsBusy = false;

        navigation.PopAsync();

      }

      public async Task<Pin> UpdateAddressAsync()
      {
        IEnumerable<Position> points = await coder.GetPositionsForAddressAsync(Account.AddressString);
        if (points != null && points.Count() > 0)
        {
          var point = points.ElementAt(0);
          Account.Latitude = point.Latitude;
          Account.Longitude = point.Longitude;
        }

        
        var address = Account.AddressString;

        var position = address != null ? new Position(Account.Latitude, Account.Longitude) : Utils.NullPosition;
        var pin = new Pin
        {
          Type = PinType.Place,
          Position = position,
          Label = Account.ToString(),
          Address = address.ToString()
        };

        return pin;
      }
    }
}
