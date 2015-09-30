//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

