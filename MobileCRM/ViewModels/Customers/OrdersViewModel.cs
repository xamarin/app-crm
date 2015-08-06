using MobileCRM.ViewModels;
using System.Collections.ObjectModel;
using MobileCRM.Models;
using MobileCRM.Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;
using MobileCRM.Extensions;
using System.Collections.Generic;
using System.Linq;

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

            Orders.AddRange(orders.OrderByDescending(x => x.IsOpen).ThenByDescending(x => x.OrderDate).ThenByDescending(x => x.ClosedDate));

            IsBusy = false;
            IsModelLoaded = true;
        }
    }
}

