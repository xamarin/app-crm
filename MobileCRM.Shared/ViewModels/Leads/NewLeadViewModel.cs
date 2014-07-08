using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MobileCRM.Shared.ViewModels.Leads
{
  public class NewLeadViewModel : BaseViewModel
  {
    IDataManager dataManager;
    INavigation navigation;
    Geocoder coder;
    public Account Account { get; set; }
    public NewLeadViewModel(INavigation navigation)
    {

      Account = new Models.Account
      {
        IsLead = true
      };

      this.Title = "New Lead";
      
      

      dataManager = DependencyService.Get<IDataManager>();
      this.navigation = navigation;
      coder = new Geocoder();
    }


    private int industryType = 0;
    public int IndustryType
    {
      get { return industryType; }
      set { industryType = value; Account.Industry = Account.IndustryTypes[industryType]; }
    }

    private Command saveLeadCommand;
    /// <summary>
    /// Command to save lead
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

    private async Task ExecuteSaveLeadCommand()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      IEnumerable<Position> points = await coder.GetPositionsForAddressAsync(Account.AddressString);
      if (points != null && points.Count() > 0)
      {
        var point = points.ElementAt(0);
        Account.Latitude = point.Latitude;
        Account.Longitude = point.Longitude;
      }



      await dataManager.SaveAccountAsync(Account);


      MessagingCenter.Send(Account, "Lead");

      IsBusy = false;

      navigation.PopAsync();

    }
  }
}
