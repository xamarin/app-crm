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
    public Job Job {get;set;}
    public string[] JobTypes = new string[] { "Mechanical", "Electrical", "Software" };
    public NewLeadViewModel(INavigation navigation)
    {

      Account = new Models.Account
      {
        IsLead = true
      };

      Job = new Models.Job
      {
        IsProposed = true,
        JobType = "Mechanical"
      };
      this.Title = "New Lead";
      
      

      dataManager = DependencyService.Get<IDataManager>();
      this.navigation = navigation;
      coder = new Geocoder();
    }

    private int jobType = 0;
    public int JobType
    {
      get { return jobType; }
      set { jobType = value; Job.JobType = JobTypes[jobType]; ; }
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
      Job.AccountId = Account.Id;
      await dataManager.SaveJobAsync(Job);

      MessagingCenter.Send(Account, "NewLead");

      IsBusy = false;

      navigation.PopAsync();

    }
  }
}
