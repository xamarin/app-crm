using MobileCRM.Shared.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCRM.Shared.ViewModels.Home;
using MobileCRM.Shared.Helpers;


namespace MobileCRM.Shared.Pages.Home
{
	public partial class DashboardView
	{
			private BarGraphHelper barData;

			private DashboardViewModel ViewModel
			{
					get { return BindingContext as DashboardViewModel; }
			}

		public DashboardView (DashboardViewModel vm)
		{
				InitializeComponent ();

				this.BindingContext = vm;

        //var items = new List<BarItem>();
        //items.Add(new BarItem { Name = "a", Value = 10 });
        //items.Add(new BarItem { Name = "b", Value = 15 });
        //items.Add(new BarItem { Name = "c", Value = 20 });
        //items.Add(new BarItem { Name = "d", Value = 5 });
        //items.Add(new BarItem { Name = "e", Value = 14 });
				//Chart.Items = items;

        //MyPie.Items = items;
		}


		private async void PopulateBarChart()
		{
				try
				{

						if (ViewModel.Orders.Count() > 0)
						{
								barData = new BarGraphHelper(ViewModel.Orders, false);


								var orderedData = (from data in barData.SalesData
																	 orderby data.DateStart
																	 select new BarItem
																	 {
																			 Name = data.DateStartString,
																			 Value = Convert.ToInt32(data.Amount)
																	 }).ToList();

								BarChart.Items = orderedData;
						} //end if

				}
				catch (Exception exc)
				{
						System.Diagnostics.Debug.WriteLine("EXCEPTION: DashboardView.PopulateBarChart(): " + exc.Message + "  |  " + exc.StackTrace);
				}

		}


    //private async void PopulatePieChart()
    //{
    //    try
    //    {

    //        if (ViewModel.Orders.Count() > 0)
    //        {
    //            var orderedData = (from data in barData.CategoryData
    //                               select new BarItem
    //                               {
    //                                   Name = data.Category,
    //                                   Value = Convert.ToInt32(data.Amount)
    //                               }).ToList();

    //            MyPie.Items = orderedData;
    //        } //end if

    //    }
    //    catch (Exception exc)
    //    {
    //        System.Diagnostics.Debug.WriteLine("EXCEPTION: DashboardView.PopulatePieChart(): " + exc.Message + "  |  " + exc.StackTrace);
    //    }
    //}



		protected async override void OnAppearing()
		{
				base.OnAppearing();

        //if (ViewModel.IsInitialized)
        //{
        //    return;
        //}

        //await ViewModel.ExecuteLoadOrdersCommand();
        await ViewModel.ExecuteLoadSeedDataCommand();

				this.PopulateBarChart();
        //this.PopulatePieChart();

				ViewModel.IsInitialized = true;

		}

	}
}
