using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinCRM.Extensions;
using XamarinCRM.Interfaces;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using XamarinCRM.Clients;

namespace XamarinCRM.ViewModels.Customers
{
    public class OrdersViewModel : BaseViewModel
    {
        public Account Account { get; private set; }

        ObservableCollection<Order> _Orders;

        public ObservableCollection<Order> Orders
        {
            get { return _Orders; }
            set
            {
                _Orders = value;
                OnPropertyChanged("Orders");
            }
        }

        readonly ICustomerDataClient _DataManager;

        public OrdersViewModel(Account account)
        {
            Account = account;

            _DataManager = DependencyService.Get<ICustomerDataClient>();

            Orders = new ObservableCollection<Order>();

            MessagingCenter.Subscribe<Order>(this, MessagingServiceConstants.SAVE_ORDER, order =>
                {
                    var index = Orders.IndexOf(order);
                    if (index >= 0)
                    {
                        Orders[index] = order;
                    }
                    else
                    {
                        Orders.Add(order);
                    }
                    Orders = new ObservableCollection<Order>(SortOrders(Orders));
                });
        }

        Command _LoadOrdersCommand;

        /// <summary>
        /// Command to load orders
        /// </summary>
        public Command LoadOrdersCommand
        {
            get
            {
                return _LoadOrdersCommand ??
                (_LoadOrdersCommand = new Command(async () =>
                        await ExecuteLoadOrdersCommand()));
            }
        }

        public async Task ExecuteLoadOrdersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var orders = new List<Order>();
            orders.AddRange(await _DataManager.GetAccountOrdersAsync(Account.Id));
            orders.AddRange(await _DataManager.GetAccountOrderHistoryAsync(Account.Id));

            Orders.Clear();
            Orders.AddRange(SortOrders(orders));

            IsBusy = false;
        }

        IEnumerable<Order> SortOrders(IEnumerable<Order> orders)
        {
            return orders.OrderByDescending(x => x.IsOpen).ThenByDescending(x => x.OrderDate).ThenByDescending(x => x.ClosedDate);
        }
    }
}

