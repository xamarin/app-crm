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
            get { return Device.OnPlatform(Palette._011, Palette._011, Color.White); }
        }

        static double ChartHeight
        {
            get { return Device.OnPlatform(160, 190, 150); }
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



            columnSeries.SetBinding(ChartSeries.ItemsSourceProperty, "WeeklySalesChartDataPoints");

            var chart = new SfChart()
            {
                HeightRequest = ChartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    LabelRotationAngle = -45,
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = MajorAxisAndLabelColor }
                },

              
            };

            if(Device.OS == TargetPlatform.Android)
                chart.BackgroundColor = Color.Transparent;

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            chart.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            // The chart has uncontrollable white space on it's left in iOS, so we're
            // wrapping it in a ContentView and adding some right padding to compensate.
            ContentView chartWrapper = new ContentView() { Content = chart };
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

