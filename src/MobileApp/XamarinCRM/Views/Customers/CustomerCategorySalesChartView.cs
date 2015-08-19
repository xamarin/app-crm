using System;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Views.Base;

namespace XamarinCRM.Views.Customers
{
    public class CustomerCategorySalesChartView : ModelTypedContentView<CustomerSalesViewModel>
    {
        public CustomerCategorySalesChartView()
        {
            PieSeries pieSeries = new PieSeries()
            {
                ConnectorLineType = ConnectorLineType.Bezier,
                DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                DataMarker = new ChartDataMarker()
            };
            pieSeries.DataMarker.LabelStyle.Margin = new Thickness(5);
            pieSeries.SetBinding(PieSeries.ItemsSourceProperty, "CategorySalesChartDataPoints");

            #region chart
            double chartHeight = Device.OnPlatform(160, 150, 150);
            SfChart chart = new SfChart()
            {
                HeightRequest = chartHeight,
                Legend = new ChartLegend(),
                BackgroundColor = Color.Transparent
            };
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            chart.Series.Add(pieSeries);
            #endregion

            Content = chart;
        }
    }
}

