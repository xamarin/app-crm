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
    //OrdersViewModel vmOrders;

		public AccountDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();


      //vmOrders = new OrdersViewModel(false, vm.Account.Id);


			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = viewModel = vm;

      this.InitChart();

      //var items = new List<BarItem>();
      //items.Add(new BarItem { Name = "July 20", Value = 10 });
      //items.Add(new BarItem { Name = "July 13", Value = 15 });
      //items.Add(new BarItem { Name = "July 6", Value = 20 });
      //items.Add(new BarItem { Name = "d", Value = 5 });
      //items.Add(new BarItem { Name = "e", Value = 14 });
      //Chart.Items = items;


		}


    private void InitChart()
    {
        var items = new List<BarItem>();
        items.Add(new BarItem { Name = "No Orders", Value = 1 });
        Chart.Items = items;
    }


    private async void PopulateChart()
    {
        try
        {
            Chart.Items.Clear();


            if (viewModel.Orders.Count() > 0)
            {

                var barData = new BarGraphHelper(viewModel.Orders, false);


                var orderedData = (from data in barData.SalesData
                                   orderby data.DateStart
                                   select new BarItem
                                   {
                                       Name = data.DateStartString,
                                       Value = Convert.ToInt32(data.Amount)
                                   }).ToList();

                Chart.Items = orderedData;
            } 
        
        }
        catch (Exception exc)
        {
            Console.WriteLine("EXCEPTION: AccountDetailsView.PopulateChart(): " + exc.Message + "  |  " + exc.StackTrace);
        }

    }   

    protected async override void OnAppearing()
    {
            try
            {

                base.OnAppearing();


                if (viewModel.IsInitialized)
                {
                    return;
                }
                
                await viewModel.ExecuteLoadOrdersCommand();
                this.PopulateChart();


                viewModel.IsInitialized = true;

            }
            catch (Exception exc)
            {
                Console.WriteLine("EXCEPTION: AccountDetailsView.OnAppearing(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        
    }



	}
}
