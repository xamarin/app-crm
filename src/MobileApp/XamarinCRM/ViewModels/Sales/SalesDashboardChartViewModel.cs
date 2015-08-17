using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Extensions;
using XamarinCRM.Helpers;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM
{
    public class SalesDashboardChartViewModel : BaseViewModel
    {
        ICustomerDataClient _CustomerDataClient;

        Command _LoadSeedDataCommand;

        ObservableCollection<Order> _Orders;
        ObservableCollection<ChartDataPoint> _SalesChartDataPoints;

        string _SalesAverage;

        public bool NeedsRefresh { get; set; }

        public SalesDashboardChartViewModel(INavigation navigation = null)
            : base(navigation)
        {
            _CustomerDataClient = DependencyService.Get<ICustomerDataClient>();

            Orders = new ObservableCollection<Order>();
            SalesChartDataPoints = new ObservableCollection<ChartDataPoint>();

            IsInitialized = false;
        }

        public Command LoadSeedDataCommand
        {
            get
            {
                return _LoadSeedDataCommand ?? (_LoadSeedDataCommand = new Command(async () => await ExecuteLoadSeedDataCommand()));
            }
        }

        public async Task ExecuteLoadSeedDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await _CustomerDataClient.SeedData();

            var orders = await _CustomerDataClient.GetAllAccountOrdersAsync();
            Orders = orders.ToObservableCollection();

            ChartHelper chartHelper = new ChartHelper(Orders, false);

            await chartHelper.ProcessData();

            SalesChartDataPoints = chartHelper
                .SalesData
                .OrderBy(x => x.DateStart)
                .Select(x => new ChartDataPoint(x.DateStart.ToString("d MMM"), x.Amount)).ToObservableCollection();

            SalesAverage = String.Format("{0:C}", SalesChartDataPoints.Average(x => x.YValue));

            IsBusy = false;
        }

        public ObservableCollection<Order> Orders
        {
            get { return _Orders; }
            set
            {
                _Orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public ObservableCollection<ChartDataPoint> SalesChartDataPoints
        {
            get { return _SalesChartDataPoints; }
            set
            {
                _SalesChartDataPoints = value;
                OnPropertyChanged("SalesChartDataPoints");
            }
        }

        public string SalesAverage
        {
            get
            {
                return _SalesAverage;
            }
            set
            {
                _SalesAverage = value;
                OnPropertyChanged("SalesAverage");
            }
        }
    }
}

