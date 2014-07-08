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
      order.ClosedDate = DateTime.Today;
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
      if (IsBusy)
        return;

      IsBusy = true;

      Order.IsOpen = false;
      await dataManager.SaveOrderAsync(Order);

      MessagingCenter.Send(Order, "Order");

      IsBusy = false;

      navigation.PopModalAsync();


    }
  }
}
