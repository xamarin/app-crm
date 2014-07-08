using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.ViewModels.Orders
{
  public class NewOrderViewModel : BaseViewModel
  {


    IDataManager dataManager;
    INavigation navigation;
    public Order Order { get; set; }
    public NewOrderViewModel(INavigation navigation, string accountId)
    {

      Order = new Models.Order
      {
        IsOpen = true,
        AccountId = accountId
      };

      this.Title = "New Order";
      
      dataManager = DependencyService.Get<IDataManager>();
      this.navigation = navigation;;
    }


    private int itemType = 0;
    public int Item
    {
      get { return itemType; }
      set { itemType = value; Order.Item = Order.ItemTypes[itemType]; }
    }


    private int itemLevel = 0;
    public int ItemLevel
    {
      get { return itemLevel; }
      set { itemLevel = value; Order.ItemLevel = Order.ItemLevels[itemLevel]; }
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


    private Command saveOrderCommand;
    /// <summary>
    /// Command to save lead
    /// </summary>
    public Command SaveOrderCommand
    {
      get
      {
        return saveOrderCommand ??
               (saveOrderCommand = new Command(async () =>
                await ExecuteSaveOrderCommand()));
      }
    }

    private async Task ExecuteSaveOrderCommand()
    {
      if (IsBusy)
        return;

      IsBusy = true;

      await dataManager.SaveOrderAsync(Order);


      MessagingCenter.Send(Order, "Order");

      IsBusy = false;

      navigation.PopAsync();


    }
  }
}
