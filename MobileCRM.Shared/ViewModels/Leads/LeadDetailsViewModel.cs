using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MobileCRM.Shared.ViewModels.Leads
{
  public class LeadDetailsViewModel : BaseViewModel
  {
    IDataManager dataManager;
    INavigation navigation;
    Geocoder coder;
    public Account Account { get; set; }
    public ObservableCollection<Job> Jobs { get; set; }

    public LeadDetailsViewModel(INavigation navigation, Account account)
    {
      if (account == null)
      {
        Account = new Models.Account();
        this.Title = "New Lead";
      }
      else
      {
        Account = account;
        this.Title = account.DisplayName;
      }

      Jobs = new ObservableCollection<Job>();
      dataManager = DependencyService.Get<IDataManager>();
      this.navigation = navigation;
      coder = new Geocoder();
    }

    private Command approveLeadCommand;
    public Command ApproveLeadCommand
    {
      get
      {
        return approveLeadCommand ??
               (approveLeadCommand = new Command(async () =>
                await ExecuteApproveLeadCommand()));
      }
    }


    private async Task ExecuteApproveLeadCommand()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      Account.IsLead = false;
      


      await dataManager.SaveAccountAsync(Account);
      //approve all jobs
      foreach(var job in Jobs)
      {
        job.IsProposed = false;
        await dataManager.SaveJobAsync(job);
      }

      MessagingCenter.Send(Account, "ApproveLead");

      IsBusy = false;

      navigation.PopModalAsync();

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


      MessagingCenter.Send(Account, "SaveLead");

      IsBusy = false;

      navigation.PopAsync();

    }

    private Command loadJobsCommand;
    /// <summary>
    /// Command to load accounts
    /// </summary>
    public Command LoadJobsCommand
    {
      get
      {
        return loadJobsCommand ??
               (loadJobsCommand = new Command(async () =>
                await ExecuteLoadJobsCommand()));
      }
    }

    private async Task ExecuteLoadJobsCommand()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      Jobs.Clear();
      var jobs = await dataManager.GetAccountJobsAsync(Account.Id, true, false);
      foreach (var job in jobs)
        Jobs.Add(job);

      IsBusy = false;

    }
  }
}
