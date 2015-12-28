// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Extensions;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class SalesDashboardChartViewModel : BaseViewModel
    {
        IDataService _DataClient;

        IChartDataService _ChartDataService;

        Command _LoadSeedDataCommand;

        ObservableCollection<Order> _Orders;
        ObservableCollection<ChartDataPoint> _WeeklySalesChartDataPoints;

        string _WeeklySalesAverage;

        public bool NeedsRefresh { get; set; }

        public SalesDashboardChartViewModel(INavigation navigation = null)
            : base(navigation)
        {
            _DataClient = DependencyService.Get<IDataService>();

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

            if (!_DataClient.IsSeeded)
            {
                await _DataClient.SeedLocalDataAsync();
            }

            Orders = (await _DataClient.GetAllOrdersAsync()).ToObservableCollection();

            WeeklySalesChartDataPoints = 
                (await _ChartDataService.GetWeeklySalesDataPointsAsync(Orders))
                    .OrderBy(x => x.DateStart)
                    .Select(x => new ChartDataPoint(FormatDateRange(x.DateStart, x.DateEnd), x.Amount)).ToObservableCollection();

            WeeklySalesAverage = String.Format("{0:C}", WeeklySalesChartDataPoints.Average(x => x.YValue));

            IsInitialized = true;
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

        static string FormatDateRange(DateTime start, DateTime end)
        {
            return String.Format("{0}-\n{1}", start.ToString("d MMM"), end.AddDays(-1).ToString("d MMM"));
        }
    }
}
