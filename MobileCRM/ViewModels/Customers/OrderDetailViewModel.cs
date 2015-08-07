using System;
using MobileCRM.ViewModels;
using MobileCRM.Interfaces;
using Xamarin.Forms;
using MobileCRM.Models;
using System.Threading.Tasks;
using MobileCRM.Statics;
using System.ComponentModel;

namespace MobileCRM
{
    public class OrderDetailViewModel : BaseViewModel
    {
        readonly IDataManager _DataManager;

        public OrderDetailViewModel(Account account, Order order = null)
        {
            Account = account;

            if (order == null)
                Order = new Order();
            else
                Order = order;
            
            _Price = Order.Price.ToString();
            _Discount = (double)Order.Discount;

            this.Title = "Order Details";

            _DataManager = DependencyService.Get<IDataManager>();

            MessagingCenter.Subscribe<CatalogProduct>(this, MessagingServiceConstants.UPDATE_ORDER_PRODUCT, catalogProduct =>
                {
                    Order.Item = catalogProduct.Name;
                    Order.Price = (int)catalogProduct.Price;
                    OnPropertyChanged("Order");
                }); 
        }

        string _Price = string.Empty;

        public string Price
        {
            get { return _Price; }
            set
            {
                var priceInt = 0;
                if (int.TryParse(value, out priceInt))
                {
                    _Price = value;
                    Order.Price = priceInt;
                }
                else
                {
                    _Price = string.Empty;
                    Order.Price = 0;
                    OnPropertyChanged("Price");
                }
            }
        }

        double _Discount = 0;

        public double Discount
        {
            get { return _Discount; }
            set
            {
                _Discount = value;
                Order.Discount = (int)_Discount;
                OnPropertyChanged("DiscountDisplay");
            }
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

        public string DiscountDisplay
        {
            get { return Order.Discount + "%"; }
        }

        int _IntItemIndex = 0;

        public int ItemIndex
        {
            get
            { 
                for (int i = 0; i < Order.ItemTypes.Length; i++)
                {
                    if (Order.Item.Equals(Order.ItemTypes[i]))
                    {
                        _IntItemIndex = i;
                        break;

                    }
                }
                return _IntItemIndex;
            }
            set
            {
                _IntItemIndex = value;
                Order.Item = Order.ItemTypes[_IntItemIndex];
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

            await _DataManager.SaveOrderAsync(Order);
            MessagingCenter.Send(Order, MessagingServiceConstants.SAVE_ORDER);
            IsBusy = false;

            PopAsync();
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

            await _DataManager.SaveOrderAsync(Order);
            MessagingCenter.Send(Order, MessagingServiceConstants.ORDER_APPROVED);
            IsBusy = false;

            await PopModalAsync();
        }
    }
}

