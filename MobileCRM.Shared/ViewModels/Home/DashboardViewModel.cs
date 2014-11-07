using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Home
{
    public class DashboardViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders
        {
            get;
            set;
        }

        IDataManager dataManager;

        public DashboardViewModel()
        {
            this.Title = "Dashboard";
            this.Icon = "dashboard.png";

            dataManager = DependencyService.Get<IDataManager>();
            Orders = new ObservableCollection<Order>();

            IsInitialized = false;

        }

        private Command loadOrdersCommand;
        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadOrdersCommand
        {
            get
            {
                return loadOrdersCommand ??
                       (loadOrdersCommand = new Command(async () =>
                        await ExecuteLoadOrdersCommand()));
            }
        }

        public async Task ExecuteLoadOrdersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Orders.Clear();

            IEnumerable<Order> orders = new List<Order>();

            orders = await dataManager.GetAllAccountOrdersAsync();

            foreach (var order in orders)
                Orders.Add(order);

            IsBusy = false;
        }



        private Command loadSeedDataCommand;

        public Command LoadSeedDataCommand
        {
            get
            {
                return loadSeedDataCommand ??
                    (loadSeedDataCommand = new Command(async () =>
                        await ExecuteLoadSeedDataCommand()));
            }
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            //await dataManager.SyncAccounts();
            //await dataManager.SyncContacts();
            //await dataManager.SyncOrders();
            await dataManager.SeedData();

            IEnumerable<Order> orders = new List<Order>();
            orders = await dataManager.GetAllAccountOrdersAsync();

            foreach (var order in orders)
                Orders.Add(order);

            IsBusy = false;
        }


    }
}
