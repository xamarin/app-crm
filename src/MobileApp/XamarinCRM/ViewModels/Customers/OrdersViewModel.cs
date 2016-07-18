
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Extensions;
using XamarinCRM.Models;
using XamarinCRM.Models.Local;
using XamarinCRM.Services;

namespace XamarinCRM.ViewModels.Customers
{
    public class OrdersViewModel : BaseViewModel
    {
        public Account Account { get; private set; }

        List<Order> _Orders;

        ObservableCollection<Grouping<Order, string>> _OrderGroups;

        public ObservableCollection<Grouping<Order, string>> OrderGroups
        {
            get { return _OrderGroups; }
            set
            {
                _OrderGroups = value;
                OnPropertyChanged("OrderGroups");
            }
        }

        readonly IDataService _DataClient;

        public OrdersViewModel(Account account)
        {
            Account = account;

            _Orders = new List<Order>();

            _DataClient = DependencyService.Get<IDataService>();

            OrderGroups = new ObservableCollection<Grouping<Order, string>>();

            MessagingCenter.Subscribe<Order>(this, MessagingServiceConstants.SAVE_ORDER, order =>
                {
                    var index = _Orders.IndexOf(order);
                    if (index >= 0)
                    {
                        _Orders[index] = order;
                    }
                    else
                    {
                        _Orders.Add(order);
                    }

                    GroupOrders();
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
            orders.AddRange(await _DataClient.GetOpenOrdersForAccountAsync(Account.Id));
            orders.AddRange(await _DataClient.GetClosedOrdersForAccountAsync(Account.Id));

            _Orders.Clear();
            _Orders.AddRange(SortOrders(orders));

            GroupOrders();

            IsBusy = false;
        }

        void GroupOrders()
        {
            OrderGroups.Clear();
            OrderGroups.AddRange(_Orders, "Status"); // The AddRange() method here is a custom extension to ObservableCollection<Grouping<K,T>>. Check out its declaration; it's pretty neat.
        }

        static IEnumerable<Order> SortOrders(IEnumerable<Order> orders)
        {
            return orders.OrderByDescending(x => x.IsOpen).ThenByDescending(x => x.ClosedDate).ThenByDescending(x => x.OrderDate);
        }
    }
}

