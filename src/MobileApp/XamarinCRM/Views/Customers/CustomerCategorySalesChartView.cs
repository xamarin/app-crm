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
            get { return Device.OnPlatform(Palette._011, Palette._011, Color.White); }
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
                        
            pieSeries.SetBinding(ChartSeries.ItemsSourceProperty, "CategorySalesChartDataPoints");
            #endregion

            #region chart
            SfChart chart = new SfChart()
            {
                HeightRequest = ChartHeight,
                Legend = new ChartLegend()
                {
                    DockPosition = LegendPlacement.Top
                },
                //BackgroundColor = Color.Transparent
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

