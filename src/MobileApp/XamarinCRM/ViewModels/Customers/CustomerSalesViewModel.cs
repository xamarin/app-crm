//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class CustomerSalesViewModel : BaseViewModel
    {
        IDataClient _DataClient;

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

            _DataClient = DependencyService.Get<IDataClient>();

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

            await _DataClient.SeedLocalDataAsync();

            Orders.Clear();
            Orders.AddRange((await _DataClient.GetAllOrdersAsync()).Where(x => x.AccountId == account.Id));

            WeeklySalesChartDataPoints.Clear();
            WeeklySalesChartDataPoints.AddRange((await _ChartDataService.GetWeeklySalesDataPointsAsync(Orders)).OrderBy(x => x.DateStart).Select(x => new ChartDataPoint(x.DateStart.ToString("d MMM"), x.Amount)));

            CategorySalesChartDataPoints.Clear();
            CategorySalesChartDataPoints.AddRange((await _ChartDataService.GetCategorySalesDataPointsAsync(Orders, _Account)).OrderBy(x => x.XValue));

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

