using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Extensions;
using XamarinCRM.Models;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM
{
    public class SalesDashboardChartViewModel : BaseViewModel
    {
        ICustomerDataClient _CustomerDataClient;

        IChartDataService _ChartDataService;

        Command _LoadSeedDataCommand;

        ObservableCollection<Order> _Orders;
        ObservableCollection<ChartDataPoint> _WeeklySalesChartDataPoints;

        string _WeeklySalesAverage;

        public bool NeedsRefresh { get; set; }

        public SalesDashboardChartViewModel(INavigation navigation = null)
            : base(navigation)
        {
            _CustomerDataClient = DependencyService.Get<ICustomerDataClient>();

            _ChartDataService = DependencyService.Get<IChartDataService>();

            Orders = new ObservableCollection<Order>();

            WeeklySalesChartDataPoints = new ObservableCollection<ChartDataPoint>();

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

            await _CustomerDataClient.SeedDataAsync();

            Orders = (await _CustomerDataClient.GetAllOrdersAsync()).ToObservableCollection();

            WeeklySalesChartDataPoints = 
                (await _ChartDataService.GetWeeklySalesDataPointsAsync(Orders))
                .OrderBy(x => x.DateStart)
                .Select(x => new ChartDataPoint(x.DateStart.ToString("d MMM"), x.Amount)).ToObservableCollection();

            WeeklySalesAverage = String.Format("{0:C}", WeeklySalesChartDataPoints.Average(x => x.YValue));

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

        public ObservableCollection<ChartDataPoint> WeeklySalesChartDataPoints
        {
            get { return _WeeklySalesChartDataPoints; }
            set
            {
                _WeeklySalesChartDataPoints = value;
                OnPropertyChanged("WeeklySalesChartDataPoints");
            }
        }

        public string WeeklySalesAverage
        {
            get { return _WeeklySalesAverage; }
            set
            {
                _WeeklySalesAverage = value;
                OnPropertyChanged("WeeklySalesAverage");
            }
        }
    }
}

