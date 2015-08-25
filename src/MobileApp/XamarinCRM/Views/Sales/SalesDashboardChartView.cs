using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Views.Sales;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

namespace XamarinCRM
{
    public class SalesDashboardChartView : ModelBoundContentView<SalesDashboardChartViewModel>
    {
        static Color MajorAxisAndLableColor
        {
            get { return Device.OnPlatform(Palette._011, Palette._008, Color.White); }
        }

        public SalesDashboardChartView()
        {
            #region sales graph header
            SalesChartHeaderView chartHeaderView = new SalesChartHeaderView() { HeightRequest = Sizes.MediumRowHeight, Padding = new Thickness(20, 10, 20, 0) };
            chartHeaderView.WeeklyAverageValueLabel.SetBinding(Label.TextProperty, "WeeklySalesAverage");
            chartHeaderView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chartHeaderView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region activity indicator
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
                HeightRequest = Sizes.MediumRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region the sales graph
            const double chartHeight = 190;

            ColumnSeries columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = MajorAxisAndLableColor },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = MajorAxisAndLableColor },
                    LabelStyle = new ChartAxisLabelStyle()
                    { 
                        TextColor = MajorAxisAndLableColor, 
                        LabelFormat = "$0"
                    }
                },
                Color = Palette._003
            };

            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, "WeeklySalesChartDataPoints");

            SfChart chart = new SfChart()
            {
                HeightRequest = chartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = MajorAxisAndLableColor }
                },
                
                BackgroundColor = Color.Transparent
            };

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            // The chart has uncontrollable white space on it's left in iOS, so we're
            // wrapping it in a ContentView and adding some right padding to compensate.
            ContentView chartWrapper = new ContentView() { Content = chart };

            StackLayout stackLayout = new UnspacedStackLayout()
            {
                Children =
                {
                    chartHeaderView,
                    loadingLabel,
                    chartActivityIndicator,
                    chartWrapper
                }
            };
            #endregion

            #region platform adjustments
            Device.OnPlatform(
                iOS: () =>
                { 
                    chartWrapper.Padding = new Thickness(0, 0, 30, 0);
                    stackLayout.BackgroundColor = Color.Transparent;
                    stackLayout.Padding = new Thickness(0, 20, 0, 0);
                }, 
                Android: () =>
                { 
                    stackLayout.BackgroundColor = Palette._009;
                    Font androidChartLabelFont = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
                    columnSeries.YAxis.LabelStyle.Font = androidChartLabelFont;
                    chart.PrimaryAxis.LabelStyle.Font = androidChartLabelFont;
                });
            #endregion

            Content = stackLayout;
        }
    }
}

