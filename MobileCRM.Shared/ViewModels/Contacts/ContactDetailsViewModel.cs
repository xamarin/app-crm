using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace MobileCRM.Shared.ViewModels.Contacts
{
    public class ContactDetailsViewModel : BaseViewModel
    {
      IDataManager dataManager;
      //INavigation navigation;
      Geocoder coder;
      public Contact Contact { get; set; }
     
      public ContactDetailsViewModel(Contact contact)
      {
        if(contact == null)
        {
          Contact = new Models.Contact();
          this.Title = "New Contact";
        }
        else
        {
          Contact = contact;
          this.Title = contact.FirstName;
        }
        
        dataManager = DependencyService.Get<IDataManager>();
        //this.navigation = navigation;
        coder = new Geocoder();
      }  //end ctor



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


      public static readonly Position NullPosition = new Position(0, 0);
      public async Task<Pin> LoadPin()
      {
          var address = Contact.AddressString;

          //Lookup Lat/Long all the time.
          //TODO: Only look up if no value, or if address properties have changed.
          //if (Contact.Latitude == 0)
          if (true)
          {
              Task<IEnumerable<Position>> getPosTask = coder.GetPositionsForAddressAsync(Contact.AddressString);
              IEnumerable<Position> pos = await getPosTask;

              if (pos.Count() > 0)
              {
                  Contact.Latitude = pos.First().Latitude;
                  Contact.Longitude = pos.Last().Longitude;
              } 
          }

          var position = address != null ? new Position(Contact.Latitude, Contact.Longitude) : NullPosition;

          var pin = new Pin
          {
              Type = PinType.Place,
              Position = position,
              Label = Contact.DisplayName,
              Address = address.ToString()
          };

          return pin;
      }

      private async Task ExecuteSaveContactCommand()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        //IEnumerable<Position> points = await coder.GetPositionsForAddressAsync(Contact.AddressString);
        //if (points != null && points.Count() > 0)
        //{
        //    var point = points.ElementAt(0);
        //    Contact.Latitude = point.Latitude;
        //    Contact.Longitude = point.Longitude;
        //}

        await dataManager.SaveContactAsync(Contact);


        MessagingCenter.Send(Contact, "SaveContact");

        IsBusy = false;

        Navigation.PopAsync();

      }


      public async Task GoBack()
      {
          await Navigation.PopAsync();
      }

    }
}
