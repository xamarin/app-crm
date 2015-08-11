using System;
using System.Diagnostics;
using System.Linq;
using MobileCRM.CustomControls;
using MobileCRM.Helpers;
using MobileCRM.ViewModels.Home;
using Xamarin;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Home
{
    public partial class DashboardView
    {
        BarGraphHelper barData;

        SalesDashboardViewModel ViewModel
        {
            get { return BindingContext as SalesDashboardViewModel; }
        }

        public DashboardView(SalesDashboardViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }

        async void PopulateBarChart()
        {
            try
            {
                if (ViewModel.Orders.Any())
                {
                    barData = new BarGraphHelper(ViewModel.Orders, false);

                    var orderedData = (barData.SalesData.OrderBy(data => data.DateStart).Select(data => new BarItem
                        {
                            Name = data.DateStartString,
                            Value = Convert.ToInt32(data.Amount)
                        })).ToList();

                    BarChart.Items = orderedData;
                } //end if

            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                Debug.WriteLine("EXCEPTION: DashboardView.PopulateBarChart(): " + exc.Message + "  |  " + exc.StackTrace);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.ExecuteLoadSeedDataCommand();

            this.PopulateBarChart();

            ViewModel.IsInitialized = true;

            Insights.Track("Dashboard Page");
        }
    }
}