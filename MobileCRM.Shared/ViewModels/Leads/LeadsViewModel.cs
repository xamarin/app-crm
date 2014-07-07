using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Leads
{
    public class LeadsViewModel : BaseViewModel
    {
      public ObservableCollection<Account> Leads
      {
        get;
        set;
      }

      IDataManager dataManager;
      public LeadsViewModel()
      {
        this.Title = "Leads";
        this.Icon = "list.png";

        dataManager = DependencyService.Get<IDataManager>();
        Leads = new ObservableCollection<Account>();

        /*MessagingCenter.Subscribe<Account>(this, "NewLead", async (account) =>
          {
            Leads.Add(account);
          });

        MessagingCenter.Subscribe<Account>(this, "ApproveLead", (account) =>
        {
          var index = Leads.IndexOf(account);
          if (index >= 0)
            Leads.RemoveAt(index);
        });*/
      }

      private Command loadLeadsCommand;
      /// <summary>
      /// Command to load accounts
      /// </summary>
      public Command LoadLeadsCommand
      {
        get
        {
          return loadLeadsCommand ??
                 (loadLeadsCommand = new Command(async () =>
                  await ExecuteLoadLeadsCommand()));
        }
      }

      private async Task ExecuteLoadLeadsCommand()
      {
        if (IsBusy)
          return;

        IsBusy = true;

        Leads.Clear();
        var accounts = await dataManager.GetAccountsAsync(true);
        foreach (var account in accounts)
          Leads.Add(account);

        IsBusy = false;

      }
    }
}
