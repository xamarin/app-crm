using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Orders
{
    public class OrderDetailsViewModel : BaseViewModel
  {


    IDataManager dataManager;
    INavigation navigation;
    public Order Order { get; set; }
    public OrderDetailsViewModel(INavigation navigation, Order order)
    {

      //order.ClosedDate = DateTime.Today;
      Order = order;
      price = order.Price.ToString();
      discount = (double)order.Discount;

      this.Title = "Order Details";
      
      dataManager = DependencyService.Get<IDataManager>();
      this.navigation = navigation;
    }

    private string price = string.Empty;
    public string Price
    {
      get { return price; }
      set
      {
        var priceInt = 0;
        if (int.TryParse(value, out priceInt))
        {
          price = value;
          Order.Price = priceInt;
        }
        else
        {
          price = string.Empty;
          Order.Price = 0;
          OnPropertyChanged("Price");
        }
      }
    }

    private double discount = 0;
    public double Discount
    {
      get { return discount; }
      set
      {
        discount = value;
        Order.Discount = (int)discount;
        OnPropertyChanged("DiscountDisplay");
      }
    }

    public string DiscountDisplay
    {
      get { return Order.Discount.ToString() + "%"; }
    }


    private int intItemIndex = 0;
    public int ItemIndex
    {
        get 
        { 
            for (int i=0; i< Order.ItemTypes.Length; i++)
            {
                if (Order.Item.Equals(Order.ItemTypes[i]))
                {
                    intItemIndex = i;
                    break;

                }
            }
            return intItemIndex;
        }
        set
        {
            intItemIndex = value;
            Order.Item = Order.ItemTypes[intItemIndex];
        }
    }


    private Command saveOrderCommand;

    public Command SaveOrderCommand
    {
        get
        {
            return saveOrderCommand ?? (saveOrderCommand = new Command(async () => await ExecuteSaveOrderCommand()));
        }
    }


    private async Task ExecuteSaveOrderCommand()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        await dataManager.SaveOrderAsync(Order);
        MessagingCenter.Send(Order, "OrderUpdate");
        IsBusy = false;

        navigation.PopAsync();
    }


    private Command approveOrderCommand;
    /// <summary>
    /// Command to save lead
    /// </summary>
    public Command ApproveOrderCommand
    {
      get
      {
        return approveOrderCommand ??
               (approveOrderCommand = new Command(async () =>
                await ExecuteApproveOrderCommand()));
      }
    }

    private async Task ExecuteApproveOrderCommand()
    {
        Order.IsOpen = false;
        //await ExecuteSaveOrderCommand();

        if (IsBusy)
            return;

        IsBusy = true;

        await dataManager.SaveOrderAsync(Order);
        MessagingCenter.Send(Order, "OrderApproved");
        IsBusy = false;

        await navigation.PopModalAsync();
    }


    public async Task GoBack()
    {
        await navigation.PopModalAsync();
    }

  }
}
