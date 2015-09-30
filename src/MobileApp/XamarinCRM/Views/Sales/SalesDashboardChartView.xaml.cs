using Xamarin.Forms;
using XamarinCRM.Views.Base;
using XamarinCRM.ViewModels.Sales;
using Syncfusion.SfChart.XForms;
using XamarinCRM.Statics;
using XamarinCRM.Converters;

namespace XamarinCRM.Views.Sales
{
    public partial class SalesDashboardChartView : SalesDashboardChartViewXaml
    {
        public SalesDashboardChartView()
        {
            InitializeComponent();

            const double chartHeight = 190;

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
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = MajorAxisAndLabelColor }
                },

                BackgroundColor = Color.Transparent
            };

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            chartWrapper.Content = chart;

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
        }

        Color MajorAxisAndLabelColor
        {
            get { return Device.OnPlatform<Color>(Palette._011, Palette._008, Color.White); }
        }
    }

    public partial class SalesDashboardChartViewXaml : ModelBoundContentView<SalesDashboardChartViewModel>
    {

    }
}

