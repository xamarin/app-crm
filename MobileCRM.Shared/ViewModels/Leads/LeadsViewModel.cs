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

        MessagingCenter.Subscribe<Account>(this, "NewLead", (account) =>
          {
            Leads.Add(account);
          });
      }

      private Command loadAccountsCommand;
      /// <summary>
      /// Command to load accounts
      /// </summary>
      public Command LoadAccountsCommand
      {
        get
        {
          return loadAccountsCommand ??
                 (loadAccountsCommand = new Command(async () =>
                  await ExecuteLoadAccountsCommand()));
        }
      }

      private async Task ExecuteLoadAccountsCommand()
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
