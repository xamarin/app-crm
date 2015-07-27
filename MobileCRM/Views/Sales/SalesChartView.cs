using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MobileCRM.Views.Sales
{
    public class SalesChartView : ContentView
    {
        SalesDashboardViewModel ViewModel
        {
            get { return BindingContext as SalesDashboardViewModel; }
        }

        readonly ColumnSeries _ColumnSeries;

        public void SetSalesChartData(IEnumerable<ChartDataPoint> salesChartDataPoints)
        {
            _ColumnSeries.ItemsSource = salesChartDataPoints;
        }

        public SalesChartView(SalesDashboardViewModel viewModel)
        {
            BindingContext = viewModel;

            BackgroundColor = Palette._008;

            double height = Device.OnPlatform(200, 190, 180);

            ActivityIndicator chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.MediumRowHeight
            };
            chartActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            chartActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");

            _ColumnSeries = new ColumnSeries()
            {
                BindingContext = ViewModel,
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._009 },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._010 },
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._009 }
                },
                Color = Palette._004
            };
            _ColumnSeries.ItemsSource = new List<ChartDataPoint>()
            {
                new ChartDataPoint("Jan", 100),
                new ChartDataPoint("Feb", 200),
                new ChartDataPoint("Mar", 300),
                new ChartDataPoint("Apr", 400),
                new ChartDataPoint("Jun", 500),
                new ChartDataPoint("Jul", 600),
            };

            // Not currently working because a binding bug in the SyncFusion ColumnSeries.ItemsSourceProperty setter
            // columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, new Binding("SalesChartDataPoints"));

            SfChart chart = new SfChart()
            {
                BindingContext = ViewModel,

                BackgroundColor = Palette._008,

                HeightRequest = height,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._009 }
                },

////                Series = new ChartSeriesCollection() { _ColumnSeries }
            };
            chart.Series.Add(_ColumnSeries);
            chart.SetBinding(IsEnabledProperty, "IsModelLoaded");
            chart.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            chart.SetBinding(IsVisibleProperty, "IsModelLoaded");

            //Initializing Primary Axis
            CategoryAxis primaryAxis = new CategoryAxis();
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            chart.SecondaryAxis = secondaryAxis;

            StackLayout stackLayout = new StackLayout()
            { 
                HeightRequest = height
            };

            stackLayout.Children.Add(chartActivityIndicator);

            stackLayout.Children.Add(chart);

            Content = stackLayout;
        }
    }
}
