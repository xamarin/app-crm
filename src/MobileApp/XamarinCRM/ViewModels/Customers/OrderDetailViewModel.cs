
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Localization;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;
using XamarinCRM.Services;

namespace XamarinCRM.ViewModels.Customers
{
    public class OrderDetailViewModel : BaseViewModel
    {
        readonly IDataService _DataClient;

        public OrderDetailViewModel(Account account, Order order = null)
        {
            Account = account;

            if (order == null)
            {
                Order = new Order() { AccountId = Account.Id };
            }
            else
            {
                Order = order;
            }

            this.Title = "Order Details";
            _DataClient = DependencyService.Get<IDataService>();

            DependencyService.Get<ILocalize>();

            MessagingCenter.Subscribe<Product>(this, MessagingServiceConstants.UPDATE_ORDER_PRODUCT, async catalogProduct =>
                {
                    Order.Item = catalogProduct.Name;
                    Order.Price = catalogProduct.Price;
                    OrderItemImageUrl = null;
                    await ExecuteLoadOrderItemImageUrlCommand(); // this is to account for Android not calling OnAppearing() when the product selection modal disappears.
                    OnPropertyChanged("Order");
                }); 
        }

        Order _Order;

        public Order Order
        {
            get { return _Order; }
            set
            {
                _Order = value;
                OnPropertyChanged("Order");
            }
        }

        Account _Account;

        public Account Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
                OnPropertyChanged("Account");
            }
        }

        string _OrderItemImageUrl;

        public string OrderItemImageUrl
        {
            get { return _OrderItemImageUrl; }
            set
            {
                _OrderItemImageUrl = value;
                OnPropertyChanged("OrderItemImageUrl");
            }
        }

        Command _SaveOrderCommand;

        public Command SaveOrderCommand
        {
            get
            {
                return _SaveOrderCommand ?? (_SaveOrderCommand = new Command(async () => await ExecuteSaveOrderCommand()));
            }
        }

        async Task ExecuteSaveOrderCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await _DataClient.SaveOrderAsync(Order);
            MessagingCenter.Send(Order, MessagingServiceConstants.SAVE_ORDER);
            IsBusy = false;

            await PopAsync();
        }

        Command _ApproveOrderCommand;

        /// <summary>
        /// Command to save lead
        /// </summary>
        public Command ApproveOrderCommand
        {
            get
            {
                return _ApproveOrderCommand ??
                (_ApproveOrderCommand = new Command(async () =>
                        await ExecuteApproveOrderCommand()));
            }
        }

        async Task ExecuteApproveOrderCommand()
        {
            Order.IsOpen = false;
            //await ExecuteSaveOrderCommand();

            if (IsBusy)
                return;

            IsBusy = true;

            await _DataClient.SaveOrderAsync(Order);
            MessagingCenter.Send(Order, MessagingServiceConstants.ORDER_APPROVED);
            IsBusy = false;

            await PopModalAsync();
        }

        Command _LoadOrderItemImageUrlCommand;

        public Command LoadOrderItemImageUrlCommand
        {
            get
            { 
                return _LoadOrderItemImageUrlCommand ??
                (_LoadOrderItemImageUrlCommand = new Command(async () =>
                        await ExecuteApproveOrderCommand()));
            }
        }

        public async Task ExecuteLoadOrderItemImageUrlCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!string.IsNullOrWhiteSpace(Order.Item))
            {
                OrderItemImageUrl = (await _DataClient.GetProductByNameAsync(Order.Item)).ImageUrl;
            }

            IsBusy = false;
        }
    }
}

