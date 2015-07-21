using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using MobileCRM.ViewModels.Home;

namespace MobileCRM.Views.Sales
{
    public class SalesChartView : ContentView
    {
        DashboardViewModel ViewModel
        {
            get { return BindingContext as DashboardViewModel; }
        }

        public SalesChartView(DashboardViewModel dashboardViewModel)
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
            chartActivityIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

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
            chart.SetBinding(ActivityIndicator.IsEnabledProperty, "IsModelLoaded");
            chart.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            chart.SetBinding(ActivityIndicator.IsVisibleProperty, "IsModelLoaded");

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
