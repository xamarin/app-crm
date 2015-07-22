using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileCRM.Interfaces;
using MobileCRM.Models;
using Xamarin.Forms;

namespace MobileCRM.ViewModels.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        ObservableCollection<Order> orders;

        public ObservableCollection<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public Account Account { get; set; }

        bool openOrders;
        string accountId;
        IDataManager dataManager;

        public OrdersViewModel(bool openOrders, string accountId)
        {
            this.accountId = accountId;
            this.openOrders = openOrders;
            this.Title = openOrders ? "Orders" : "History";
            this.Icon = openOrders ? "order.png" : "orderhistory.png";

            dataManager = DependencyService.Get<IDataManager>();
            Orders = new ObservableCollection<Order>();

            if (openOrders)
            {
                MessagingCenter.Subscribe<Order>(this, MessagingServiceConstants.ORDER_UPDATE, (order) =>
                    {
                        IsInitialized = false;
                    });
            }
            else
            {
                MessagingCenter.Subscribe<Order>(this, MessagingServiceConstants.ORDER_APPROVED, async (order) =>
                    {
                        IsInitialized = false;
                    });
            }
        }

        Command loadOrdersCommand;

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

            if (openOrders)
            {
                orders = await dataManager.GetAccountOrdersAsync(accountId);    
            }
            else
            {
                orders = await dataManager.GetAccountOrderHistoryAsync(accountId);
            }

            ObservableCollection<Order> ordersSorted = new ObservableCollection<Order>();
            foreach (var order in orders)
            {
                ordersSorted.Add(order);
            }

            Orders = ordersSorted;

            IsBusy = false;
        }
    }
}
