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
      INavigation navigation;
      Geocoder coder;
      public Contact Contact { get; set; }
     
      public ContactDetailsViewModel(INavigation navigation, Contact contact)
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

        IEnumerable<Position> points = await coder.GetPositionsForAddressAsync(Contact.AddressString);
        if(points != null && points.Count() > 0)
        {
          var point = points.ElementAt(0);
          Contact.Latitude = point.Latitude;
          Contact.Longitude = point.Longitude;
        }
        await dataManager.SaveContactAsync(Contact);

        MessagingCenter.Send(Contact, "NewContact");

        IsBusy = false;

        navigation.PopAsync();

      }
    }
}
