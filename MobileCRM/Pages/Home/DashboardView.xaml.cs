using System;
using System.Diagnostics;
using System.Linq;
using MobileCRM.CustomControls;
using MobileCRM.Helpers;
using MobileCRM.ViewModels.Home;
using Xamarin;

namespace MobileCRM.Pages.Home
{
    public partial class DashboardView
    {
        BarGraphHelper barData;

        DashboardViewModel ViewModel
        {
            get { return BindingContext as DashboardViewModel; }
        }

        public DashboardView(DashboardViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }

        async void PopulateBarChart()
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