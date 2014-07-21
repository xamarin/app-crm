using MobileCRM.CustomControls;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Orders;
using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountDetailsView
	{
		AccountDetailsViewModel viewModel;
    OrdersViewModel vmOrders;

		public AccountDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();


      vmOrders = new OrdersViewModel(false, vm.Account.Id);


			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = vm;


      //var items = new List<BarItem>();
      //items.Add(new BarItem { Name = "July 20", Value = 10 });
      //items.Add(new BarItem { Name = "July 13", Value = 15 });
      //items.Add(new BarItem { Name = "July 6", Value = 20 });
      //items.Add(new BarItem { Name = "d", Value = 5 });
      //items.Add(new BarItem { Name = "e", Value = 14 });
      //Chart.Items = items;



      //Task t = vmOrders.ExecuteLoadOrdersCommand();
      this.DrawGraph();

		}


    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (vmOrders.IsInitialized)
        {
            return;
        }
        

        vmOrders.IsInitialized = true;
    }


    private async void DrawGraph()
    {
        IDataManager dataManager = DependencyService.Get<IDataManager>();
        IEnumerable<Order> orders = await dataManager.GetAccountOrderHistoryAsync(viewModel.Account.Id);

        


        var items = new List<BarItem>();
            
        BarGraphHelper b = new BarGraphHelper(orders, false);
        for (int i=b.SalesData.Count-1; i>=0; i--)
        {
            WeeklySalesData salesData = b.SalesData.ElementAt(i);
            items.Add(new BarItem() { Name = salesData.DateEndString, Value = Convert.ToInt32(salesData.Amount) });
        } //end foreach

        Chart.Items = items;

    } //end DrawGraph

	}
}
