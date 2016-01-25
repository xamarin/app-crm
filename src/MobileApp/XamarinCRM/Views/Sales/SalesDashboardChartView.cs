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
using XamarinCRM.Statics;
using XamarinCRM.Views.Sales;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

namespace XamarinCRM
{
    public class SalesDashboardChartView : ModelBoundContentView<SalesDashboardChartViewModel>
    {
        public SalesDashboardChartView()
        {
            #region sales graph header
            var chartHeaderView = new SalesChartHeaderView() { HeightRequest = RowSizes.MediumRowHeightDouble, Padding = new Thickness(20, 10, 20, 0) };
            chartHeaderView.SetBinding(SalesChartHeaderView.WeeklyAverageProperty, "WeeklySalesAverage");
            chartHeaderView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chartHeaderView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region activity indicator
            var chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = RowSizes.MediumRowHeightDouble
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
                HeightRequest = RowSizes.MediumRowHeightDouble,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region the sales graph
            double chartHeight = Device.OnPlatform(190, 250, 190);

            var columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    Title = new ChartAxisTitle()
                    {
                        Text = TextResources.SalesDashboard_SalesChart_YAxis_Title,
                        Font = ChartAxisFont,
                        TextColor = Palette._011
                    },
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = AxisLineColor },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = AxisLineColor },
                    LabelStyle = new ChartAxisLabelStyle()
                    { 
                        TextColor = AxisLabelColor, 
                        LabelFormat = "$0"
                    }
                },
                DataMarker = new ChartDataMarker()
                {
                    LabelStyle = new DataMarkerLabelStyle()
                    {
                        LabelPosition = DataMarkerLabelPosition.Auto,
                        TextColor = Color.Black, 
                        BackgroundColor = Color.Transparent, //Palette._003,
                        LabelFormat = "$0.00"
                    }
                },
                DataMarkerPosition = DataMarkerPosition.Top,
                EnableDataPointSelection = false,
                Color = Palette._003
            };

            columnSeries.SetBinding(ChartSeries.ItemsSourceProperty, "WeeklySalesChartDataPoints");

            var chart = new SfChart()
            {
                HeightRequest = chartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    Title = new ChartAxisTitle()
                    {
                        Text = TextResources.SalesDashboard_SalesChart_PrimaryAxis_Title,
                        Font = ChartAxisFont,
                        TextColor = Palette._011
                    },
                    LabelRotationAngle = -45,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = AxisLabelColor }
                },        
            };

            if (Device.OS == TargetPlatform.Android)
                chart.BackgroundColor = Color.Transparent;

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
           

            StackLayout stackLayout = new StackLayout()
            {
                Spacing = 0,
                Children =
                {
                    loadingLabel,
                    chartActivityIndicator,
                    chartHeaderView,
                    new ContentView() { Content = chart, HeightRequest = chartHeight }
                }
            };
            #endregion

            #region platform adjustments
            Device.OnPlatform(
                iOS: () =>
                { 
                    columnSeries.DataMarker.LabelStyle.Font = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * 0.6);
                }, 
                Android: () =>
                { 
                    columnSeries.YAxis.LabelStyle.Font = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
                    columnSeries.DataMarker.LabelStyle.Font = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.2);
                    chart.PrimaryAxis.LabelStyle.Font = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
                });
            #endregion

            Content = stackLayout;
        }

        static Color AxisLabelColor
        {
            get { return Device.OnPlatform(Palette._011, Palette._011, Color.White); }
        }

        static Color AxisLineColor
        {
            get { return Device.OnPlatform(Palette._008, Palette._008, Color.White); }
        }

        static Font ChartAxisFont
        {
            get
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    return Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Default, typeof(ChartAxisTitle)) * 0.6);
                }
                else if (Device.OS == TargetPlatform.Android)
                {
                    return Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Default, typeof(ChartAxisTitle)) * 1.7);
                }
                else
                {
                    return Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Default, typeof(ChartAxisTitle)));
                }
            }
        }
    }
}
