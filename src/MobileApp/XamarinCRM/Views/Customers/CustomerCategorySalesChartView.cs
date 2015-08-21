using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Views.Base;
using System.Collections.Generic;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Customers
{
    public class CustomerCategorySalesChartView : ModelBoundContentView<CustomerSalesViewModel>
    {
        static double ChartHeight
        {
            get { return Device.OnPlatform(175, 200, 175); }
        }

        static Color LegendLabelColor
        {
            get { return Device.OnPlatform(Palette._011, Palette._008, Color.White); }
        }

        public CustomerCategorySalesChartView()
        {
            #region chart series
            PieSeries pieSeries = new PieSeries()
            {
                ExplodeAll = true,
                ConnectorLineType = ConnectorLineType.Bezier,
                DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended,
                DataMarker = new ChartDataMarker()
                { 
                    LabelStyle = new DataMarkerLabelStyle()
                    { 
                        Margin = new Thickness(5),
                        LabelFormat = "$0.00"
                    }
                },
                ColorModel = new ChartColorModel()
                {
                    Palette = ChartColorPalette.Custom,
                    CustomBrushes = new List<Color>()
                    { 
                        Palette._014, 
                        Palette._015, 
                        Palette._016
                    }
                }
            };
                        
            pieSeries.SetBinding(PieSeries.ItemsSourceProperty, "CategorySalesChartDataPoints");
            #endregion

            #region chart
            SfChart chart = new SfChart()
            {
                HeightRequest = ChartHeight,
                Legend = new ChartLegend()
                {
                    DockPosition = LegendPlacement.Top
                },
                BackgroundColor = Color.Transparent
            };
            chart.Legend.LabelStyle.TextColor = LegendLabelColor;
            
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            chart.Series.Add(pieSeries);
            #endregion

            #region platform adjustments
            Device.OnPlatform(
                Android: () =>
                {
                    Font androidChartLabelFont = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
                    pieSeries.DataMarker.LabelStyle.Font = androidChartLabelFont;
                });
            #endregion

            Content = chart;
        }
    }
}

