using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> orders;

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

      //public bool NeedsRefresh { get; set; }

      private bool openOrders;
      private string accountId;
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
              MessagingCenter.Subscribe<Order>(this, "OrderUpdate", (order) =>
              {
                  IsInitialized = false;
              });
          }
          else
          {
              MessagingCenter.Subscribe<Order>(this, "OrderApproved", async (order) =>
              {
                  IsInitialized = false;


              });
          }

       

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

        if (openOrders)
        {
            orders = await dataManager.GetAccountOrdersAsync(accountId);    
        } else {
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
