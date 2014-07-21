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
      public ObservableCollection<Order> Orders
      {
        get;
        set;
      }

      public bool NeedsRefresh { get; set; }
      private bool openOrders;
      private string accountId;
      IDataManager dataManager;

      public OrdersViewModel(bool openOrders, string accountId)
      {
        this.accountId = accountId;
        this.openOrders = openOrders;
        this.Title = openOrders ? "Orders" : "History";
        this.Icon = "list.png";

        dataManager = DependencyService.Get<IDataManager>();
        Orders = new ObservableCollection<Order>();

        MessagingCenter.Subscribe<Order>(this, "Order", (account) =>
          {
            IsInitialized = false;
          });

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
        
        foreach (var order in orders)
          Orders.Add(order);

        IsBusy = false;

      }
    }
}
