using MobileCRM.Interfaces;
using MobileCRM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using MobileCRM.Models;
using Xamarin.Forms.Maps;

namespace MobileCRM.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel
    {
        public ObservableCollection<Account> Accounts
        {
            get;
            set;
        }

        IDataManager dataManager;

        public AccountsViewModel()
        {
            this.Title = "Accounts";
            this.Icon = "list.png";

            dataManager = DependencyService.Get<IDataManager>();
            Accounts = new ObservableCollection<Account>();

            MessagingCenter.Subscribe<Account>(this, "Account", (account) =>
                {
                    IsInitialized = false;
                });
        }

        Command loadAccountsCommand;

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

        async Task ExecuteLoadAccountsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Accounts.Clear();
            var accounts = await dataManager.GetAccountsAsync(false);
            foreach (var account in accounts)
                Accounts.Add(account);

            IsBusy = false;
        }

        public static readonly Position NullPosition = new Position(0, 0);

        public List<Pin> LoadPins()
        {
            var pins = Accounts.Select(model =>
                {
                    var item = model;
                    var address = item.AddressString;

                    var position = address != null ? new Position(item.Latitude, item.Longitude) : NullPosition;
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = item.ToString(),
                        Address = address.ToString()
                    };
                    return pin;
                }).ToList();

            return pins; 
        }
    }
}