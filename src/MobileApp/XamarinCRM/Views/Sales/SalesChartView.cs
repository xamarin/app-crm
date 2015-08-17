using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Views.Sales;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

namespace XamarinCRM
{
    public class SalesChartView : ModelTypedContentView<SalesDashboardChartViewModel>
    {
        public SalesChartView()
        {
            #region sales graph header
            SalesChartHeaderView chartHeaderView = new SalesChartHeaderView() { HeightRequest = Sizes.MediumRowHeight };
            chartHeaderView.WeeklyAverageValueLabel.SetBinding(Label.TextProperty, "SalesAverage");
            chartHeaderView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chartHeaderView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region activity indiciator
            ActivityIndicator chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.MediumRowHeight
            };
            chartActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region loading label
            Label loadingLabel = new Label()
            {
                Text = TextResources.SalesDashboard_SalesChart_LoadingLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = Sizes.SmallRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center,
                TextColor = Palette._009
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region the sales graph
            double chartHeight = Device.OnPlatform(190, 190, 180);

            StackLayout stackLayout = new UnspacedStackLayout() { HeightRequest = chartHeight + Sizes.MediumRowHeight };
            Device.OnPlatform(iOS: () => stackLayout.BackgroundColor = Color.Transparent, Android: () => stackLayout.BackgroundColor = Palette._010);



            ColumnSeries columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._011 },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._012 },
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._011 }
                },
                Color = Palette._004
            };

            // Not currently working because a binding bug in the SyncFusion ColumnSeries.ItemsSourceProperty setter
            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, "SalesChartDataPoints");

            SfChart chart = new SfChart()
            {
                HeightRequest = chartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._011 }
                }
            };
            Device.OnPlatform(
                iOS: () => chart.BackgroundColor = Color.Transparent, 
                Android: () => chart.BackgroundColor = Palette._010);

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            // The chart has uncontrollable white space on it's left in iOS, so we're
            // wrapping it in a ContentView and adding some right padding to compensate.
            ContentView chartWrapper = new ContentView() { Content = chart };
            Device.OnPlatform(iOS: () => chartWrapper.Padding = new Thickness(0, 0, 30, 0));

            stackLayout.Children.Add(chartHeaderView);
            stackLayout.Children.Add(loadingLabel);
            stackLayout.Children.Add(chartActivityIndicator);
            stackLayout.Children.Add(chartWrapper);
            #endregion

            Device.OnPlatform(iOS: () => stackLayout.Padding = new Thickness(0, 20, 0, 0));

            Content = stackLayout;
        }
    }
}

