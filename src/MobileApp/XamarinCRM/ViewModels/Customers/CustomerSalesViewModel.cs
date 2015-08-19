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
    public class CustomerSalesViewModel : BaseViewModel
    {
        ICustomerDataClient _CustomerDataClient;

        IChartDataService _ChartDataService;

        Command _LoadSeedDataCommand;

        Account _Account;

        public Account Account
        {
            get { return _Account; }
        }

        string _WeeklySalesAverage;

        public bool NeedsRefresh { get; set; }

        public CustomerSalesViewModel(Account account, INavigation navigation = null)
            : base(navigation)
        {
            _Account = account;

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
                return _LoadSeedDataCommand ?? (_LoadSeedDataCommand = new Command(async () => await ExecuteLoadSeedDataCommand(_Account)));
            }
        }

        public async Task ExecuteLoadSeedDataCommand(Account account)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await _CustomerDataClient.SeedData();

            Orders = (await _CustomerDataClient.GetAllAccountOrdersAsync())
                .Where(x => x.AccountId == account.Id)
                .ToObservableCollection();

            WeeklySalesChartDataPoints = 
                (await _ChartDataService.GetWeeklySalesDataPoints(Orders))
                    .OrderBy(x => x.DateStart)
                    .Select(x => new ChartDataPoint(x.DateStart.ToString("d MMM"), x.Amount)).ToObservableCollection();

            CategorySalesChartDataPoints = 
                (await _ChartDataService.GetCategorySalesDataPoints(Orders, _Account))
                    .OrderBy(x => x.XValue)
                    .ToObservableCollection();

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

