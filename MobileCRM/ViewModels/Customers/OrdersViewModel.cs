using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MobileCRM.Extensions;
using MobileCRM.Interfaces;
using MobileCRM.Models;
using MobileCRM.Statics;
using MobileCRM.ViewModels.Base;
using Xamarin.Forms;

namespace MobileCRM.Customers
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

        readonly IDataManager _DataManager;

        public OrdersViewModel(Account account)
        {
            Account = account;

            _DataManager = DependencyService.Get<IDataManager>();

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
            IsModelLoaded = false;

            Orders.Clear();
            var orders = new List<Order>();
            orders.AddRange(await _DataManager.GetAccountOrdersAsync(Account.Id));
            orders.AddRange(await _DataManager.GetAccountOrderHistoryAsync(Account.Id));

            Orders.AddRange(SortOrders(orders));

            IsBusy = false;
            IsModelLoaded = true;
        }

        IEnumerable<Order> SortOrders(IEnumerable<Order> orders)
        {
            return orders.OrderByDescending(x => x.IsOpen).ThenByDescending(x => x.OrderDate).ThenByDescending(x => x.ClosedDate);
        }
    }
}

