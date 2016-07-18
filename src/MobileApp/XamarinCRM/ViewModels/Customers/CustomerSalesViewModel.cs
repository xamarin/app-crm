
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Models;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM
{
    public class CustomerSalesViewModel : BaseViewModel
    {
        IDataService _DataClient;

        IChartDataService _ChartDataService;

        Command _LoadSeedDataCommand;

        Account _Account;

        public Account Account
        {
            get { return _Account; }
        }

        string _WeeklySalesAverage;

        public string WeeklySalesAverage
        {
            get { return _WeeklySalesAverage; }
            set
            {
                _WeeklySalesAverage = value;
                OnPropertyChanged("WeeklySalesAverage");
            }
        }

        static string FormatDateRange(DateTime start, DateTime end)
        {
            return String.Format("{0}-\n{1}", start.ToString("d MMM"), end.AddDays(-1).ToString("d MMM"));
        }

        public bool NeedsRefresh { get; set; }

        public CustomerSalesViewModel(Account account)
        {
            _Account = account;

            _DataClient = DependencyService.Get<IDataService>();

            _ChartDataService = DependencyService.Get<IChartDataService>();

            Orders = new ObservableCollection<Order>();

            WeeklySalesChartDataPoints = new ObservableCollection<ChartDataPoint>();

            CategorySalesChartDataPoints = new ObservableCollection<ChartDataPoint>();

            IsInitialized = false;
        }

        public Command LoadSeedDataCommand
        {
            get
            {
                return _LoadSeedDataCommand ?? (_LoadSeedDataCommand = new Command(async () => await ExecuteLoadSeedDataCommand(_Account)));
            }
        }

        public async Task ExecuteLoadSeedDataCommand(Account account)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!_DataClient.IsSeeded)
            {
                await _DataClient.SeedLocalDataAsync();
            }

            Orders.Clear();
            Orders.AddRange((await _DataClient.GetAllOrdersAsync()).Where(x => x.AccountId == account.Id));

            WeeklySalesChartDataPoints.Clear();
            WeeklySalesChartDataPoints.AddRange((await _ChartDataService.GetWeeklySalesDataPointsAsync(Orders)).OrderBy(x => x.DateStart).Select(x => new ChartDataPoint(FormatDateRange(x.DateStart, x.DateEnd), x.Amount)));

            var weeklyTotal = WeeklySalesChartDataPoints.Sum(x => x.YValue);

            CategorySalesChartDataPoints.Clear();
            CategorySalesChartDataPoints.AddRange((await _ChartDataService.GetCategorySalesDataPointsAsync(Orders)).Select(x => new ChartDataPoint(x.Key, x.Sum(y => y.Amount))).OrderBy(x => x.XValue));

            var categoriesTotal = CategorySalesChartDataPoints.Sum(x => x.YValue);

            WeeklySalesAverage = String.Format("{0:C}", WeeklySalesChartDataPoints.Average(x => x.YValue));

            IsBusy = false;
        }

        ObservableCollection<Order> _Orders;

        public ObservableCollection<Order> Orders
        {
            get { return _Orders; }
            set
            {
                _Orders = value;
                OnPropertyChanged("Orders");
            }
        }

        ObservableCollection<ChartDataPoint> _WeeklySalesChartDataPoints;

        public ObservableCollection<ChartDataPoint> WeeklySalesChartDataPoints
        {
            get { return _WeeklySalesChartDataPoints; }
            set
            {
                _WeeklySalesChartDataPoints = value;
                OnPropertyChanged("WeeklySalesChartDataPoints");
            }
        }

        ObservableCollection<ChartDataPoint> _CategorySalesChartDataPoints;

        public ObservableCollection<ChartDataPoint> CategorySalesChartDataPoints
        {
            get
            { 
                return _CategorySalesChartDataPoints; 
            }
            set
            {
                _CategorySalesChartDataPoints = value;
                OnPropertyChanged("CategorySalesChartDataPoints");
            }
        }
    }
}

