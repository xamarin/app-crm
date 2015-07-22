using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Views.Sales
{
    public class SalesChartView : ContentView
    {
        SalesDashboardViewModel ViewModel
        {
            get { return BindingContext as SalesDashboardViewModel; }
        }

        public SalesChartView(SalesDashboardViewModel dashboardViewModel)
        {
            BindingContext = dashboardViewModel;

            double height = Device.OnPlatform<double>(200, 180, 190);

            Padding = Device.OnPlatform(
                new Thickness(0, 0, 20, 0),
                new Thickness(20, 0, 20, 0),
                new Thickness(10)
            );

            BackgroundColor = Palette._008;

            ActivityIndicator chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.MediumRowHeight
            };
            chartActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            chartActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");

            ColumnSeries columnSeries = new ColumnSeries()
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
            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, new Binding("SalesChartDataPoints"));

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

                Series = new ChartSeriesCollection() { columnSeries }
            };
            chart.SetBinding(IsEnabledProperty, "IsModelLoaded");
            chart.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            chart.SetBinding(IsVisibleProperty, "IsModelLoaded");

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
