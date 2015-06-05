using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Leads
{
    public class LeadsViewModel : BaseViewModel
    {
        ObservableCollection<Account> leads;

        public ObservableCollection<Account> Leads
        {
            get
            {
                return leads;
            }
            set
            {
                leads = value;
                OnPropertyChanged("Leads");
            }
        }

        public bool NeedsRefresh { get; set; }

        IDataManager dataManager;

        public LeadsViewModel()
        {
            this.Title = "Leads";
            this.Icon = "list.png";

            dataManager = DependencyService.Get<IDataManager>();
            Leads = new ObservableCollection<Account>();

            MessagingCenter.Subscribe<Account>(this, "SaveAccount", (account) =>
                {
                    var index = Leads.IndexOf(account);
                    if (index >= 0)
                    {
                        Leads[index] = account;
                    }
                    else
                    {
                        Leads.Add(account);
                    }
                    Leads = new ObservableCollection<Account>(
                        from l in Leads
                        orderby l.Company
                        select l);
                });
        }

        Command loadLeadsCommand;

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

        async Task ExecuteLoadLeadsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Leads.Clear();
            IEnumerable<Account> accounts = await dataManager.GetAccountsAsync(true);
            foreach (var account in accounts)
                Leads.Add(account);

            IsBusy = false;
        }
    }
}