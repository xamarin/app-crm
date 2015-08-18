using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Extensions;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Services;

namespace XamarinCRM
{
    public class SalesDashboardChartViewModel : BaseViewModel
    {
        ICustomerDataClient _CustomerDataClient;

        IChartDataService _ChartDataService;

        Command _LoadSeedDataCommand;

        ObservableCollection<Order> _Orders;
        ObservableCollection<ChartDataPoint> _SalesChartDataPoints;

        string _SalesAverage;

        public bool NeedsRefresh { get; set; }

        public SalesDashboardChartViewModel(INavigation navigation = null)
            : base(navigation)
        {
            _CustomerDataClient = DependencyService.Get<ICustomerDataClient>();

            _ChartDataService = DependencyService.Get<IChartDataService>();

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

            Orders = (await _CustomerDataClient.GetAllAccountOrdersAsync()).ToObservableCollection();

            SalesChartDataPoints = 
                (await _ChartDataService.GetWeeklySalesDataPoints(Orders))
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

