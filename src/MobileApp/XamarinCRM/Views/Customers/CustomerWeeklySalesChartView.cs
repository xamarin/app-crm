using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Sales;

namespace XamarinCRM.Views.Customers
{
    public class CustomerWeeklySalesChartView : ModelBoundContentView<CustomerSalesViewModel>
    {
        static Color MajorAxisAndLabelColor
        {
            get { return Device.OnPlatform(Palette._011, Palette._008, Color.White); }
        }

        static double ChartHeight
        {
            get { return Device.OnPlatform(160, 150, 150); }
        }

        public CustomerWeeklySalesChartView()
        {
            #region leads list activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.MediumRowHeight
            };
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region loading label
            Label loadingLabel = new Label()
            {
                Text = TextResources.SalesDashboard_SalesChart_LoadingLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = Sizes.MediumRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region sales graph header
            SalesChartHeaderView chartHeaderView = new SalesChartHeaderView() { HeightRequest = Sizes.MediumRowHeight };
            chartHeaderView.WeeklyAverageValueLabel.SetBinding(Label.TextProperty, "WeeklySalesAverage");
            chartHeaderView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chartHeaderView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region chart

            ColumnSeries columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = MajorAxisAndLabelColor },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = MajorAxisAndLabelColor },
                    LabelStyle = new ChartAxisLabelStyle()
                    { 
                        TextColor = MajorAxisAndLabelColor,
                        LabelFormat = "$0"
                    }
                },
                Color = Palette._014
            };


            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, "WeeklySalesChartDataPoints");

            SfChart chart = new SfChart()
            {
                HeightRequest = ChartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = MajorAxisAndLabelColor }
                },

                BackgroundColor = Color.Transparent
            };

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            // The chart has uncontrollable white space on it's left in iOS, so we're
            // wrapping it in a ContentView and adding some right padding to compensate.
            ContentView chartWrapper = new ContentView() { Content = chart, BackgroundColor = Color.Transparent };
            #endregion

            #region compsose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout();
            stackLayout.Children.Add(loadingLabel);
            stackLayout.Children.Add(activityIndicator);
            stackLayout.Children.Add(chartHeaderView);
            stackLayout.Children.Add(chartWrapper);
            #endregion

            #region platform adjustments
            Device.OnPlatform(
                iOS: () =>
                {
                    chartWrapper.Padding = new Thickness(0, 0, 30, 0);
                },
                Android: () =>
                {
                    Font androidChartLabelFont = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
                    columnSeries.YAxis.LabelStyle.Font = androidChartLabelFont;
                    chart.PrimaryAxis.LabelStyle.Font = androidChartLabelFont;
                });
            #endregion

            Content = stackLayout;
        }
    }
}

