using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MobileCRMAndroid.Renderers;
using System.ComponentModel;

using OxyPlot.XamarinAndroid;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


[assembly: ExportRenderer(typeof(MobileCRM.CustomControls.BarChart), typeof(BarChartRenderer))]
namespace MobileCRMAndroid.Renderers
{


    public class BarChartRenderer : ViewRenderer<MobileCRM.CustomControls.BarChart, PlotView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomControls.BarChart> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            var plotModel1 = new PlotModel();


            plotModel1.LegendBorderThickness = 0;
            plotModel1.LegendOrientation = LegendOrientation.Horizontal;
            plotModel1.LegendPlacement = LegendPlacement.Outside;
            plotModel1.LegendPosition = LegendPosition.BottomCenter;

            var categoryAxis1 = new CategoryAxis();
            plotModel1.Axes.Add(categoryAxis1);

            var linearAxis1 = new LinearAxis();
            linearAxis1.AbsoluteMinimum = 0;
            linearAxis1.MaximumPadding = 0.06;
            linearAxis1.MinimumPadding = 0;
            plotModel1.Axes.Add(linearAxis1);

            var columnSeries1 = new ColumnSeries();
            columnSeries1.LabelFormatString = "{0}";
            columnSeries1.LabelPlacement = LabelPlacement.Inside;
            columnSeries1.StrokeThickness = 1;
            plotModel1.Series.Add(columnSeries1);

            foreach (var item in Element.Items)
            {

                columnSeries1.Items.Add(new ColumnItem(item.Value, -1) { Color = OxyColors.Orange });
                categoryAxis1.ActualLabels.Add(item.Name);
            }

            var plotView = new PlotView(Forms.Context);

            plotView.Model = plotModel1;
            SetNativeControl(plotView);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            if (e.PropertyName == MobileCRM.CustomControls.BarChart.ItemsProperty.PropertyName)
            {
                var columnSeries1 = Control.Model.Series[0] as ColumnSeries;
                var categoryAxis1 = Control.Model.Axes[0] as CategoryAxis;

                columnSeries1.Items.Clear();
                categoryAxis1.Labels.Clear();
                categoryAxis1.ActualLabels.Clear();
                foreach (var item in Element.Items)
                {

                    //columnSeries1.Items.Add(new ColumnItem(item.Value));
                    columnSeries1.Items.Add(new ColumnItem(item.Value, -1) { Color = OxyColors.Orange });
                    //categoryAxis1.Labels.Add(item.Name);
                    categoryAxis1.ActualLabels.Add(item.Name);
                }



                //InvokeOnMainThread(() =>
                //{
                Control.Model.InvalidatePlot(true);
                Control.InvalidatePlot(true);
                //});
            }
        }

    }
}




    //OXYPLOT LOGARITHMIC
//    public class BarChartRenderer : ViewRenderer<MobileCRM.CustomControls.BarChart, PlotView>
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomControls.BarChart> e)
//        {
//            base.OnElementChanged(e);
//            if (e.OldElement != null || this.Element == null)
//                return;

//            var plotModel1 = new PlotModel();
//            plotModel1.LegendBorderThickness = 0;
//            plotModel1.LegendOrientation = LegendOrientation.Horizontal;
//            plotModel1.LegendPlacement = LegendPlacement.Outside;
//            plotModel1.LegendPosition = LegendPosition.BottomCenter;
//            plotModel1.Title = string.Empty;
//            var categoryAxis1 = new CategoryAxis();
//            categoryAxis1.MinorStep = 1;
//            plotModel1.Axes.Add(categoryAxis1);
//            var logarithmicAxis1 = new LogarithmicAxis();
//            logarithmicAxis1.AbsoluteMinimum = 0;
//            logarithmicAxis1.Minimum = 0.1;
//            logarithmicAxis1.MinimumPadding = 0;
//            plotModel1.Axes.Add(logarithmicAxis1);
//            var columnSeries1 = new ColumnSeries();
//            columnSeries1.BaseValue = 0.1;
//            columnSeries1.StrokeThickness = 1;
//            plotModel1.Series.Add(columnSeries1);

//            foreach (var item in Element.Items)
//            {

//                columnSeries1.Items.Add(new ColumnItem(item.Value));
//                //categoryAxis1.Labels.Add(item.Name);
//                categoryAxis1.ActualLabels.Add(item.Name);
//            }

//            //var plotView = new PlotView();
//            var plotView = new PlotView(Forms.Context);

//            plotView.Model = plotModel1;
//            SetNativeControl(plotView);
//        }
//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);
//            if (this.Element == null || this.Control == null)
//                return;
//            if (e.PropertyName == MobileCRM.CustomControls.BarChart.ItemsProperty.PropertyName)
//            {
//                var columnSeries1 = Control.Model.Series[0] as ColumnSeries;
//                var categoryAxis1 = Control.Model.Axes[0] as CategoryAxis;

//                columnSeries1.Items.Clear();
//                categoryAxis1.Labels.Clear();
//                categoryAxis1.ActualLabels.Clear();
//                foreach (var item in Element.Items)
//                {

//                    columnSeries1.Items.Add(new ColumnItem(item.Value));
//                    //categoryAxis1.Labels.Add(item.Name);
//                    categoryAxis1.ActualLabels.Add(item.Name);
//                }

//                //InvokeOnMainThread(() =>
//                //{
//                    Control.Model.InvalidatePlot(true);
//                    Control.InvalidatePlot(true);
//                //});
//            }
//        }

//    }
//}




//OLD COMPONENT BAR CHART

//  public class BarChartRenderer : ViewRenderer<MobileCRM.CustomControls.BarChart, BarChart.BarChartView>
//  {
//    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomControls.BarChart> e)
//    {
//      base.OnElementChanged(e);
//      if (e.OldElement != null || this.Element == null)
//        return;
//      var barChart = new BarChart.BarChartView(Forms.Context)
//      {
//        LegendColor = Android.Graphics.Color.Black,
//        BarCaptionOuterColor = Android.Graphics.Color.Black,
//        BarCaptionInnerColor = Android.Graphics.Color.Black,
//        ItemsSource = Element.Items.Select(item => new BarChart.BarModel
//        {
//          Value = item.Value,
//          Legend = item.Name,
//        })
//      };
//      SetNativeControl(barChart);
//    }
//    protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e) {
//      base.OnElementPropertyChanged(sender, e);
//      if (this.Element == null || this.Control == null)
//        return;
//      if (e.PropertyName == MobileCRM.CustomControls.BarChart.ItemsProperty.PropertyName)
//      {
//        Control.ItemsSource = Element.Items.Select(item => new BarChart.BarModel
//        {
//          Value = item.Value,
//          Legend = item.Name
//        });
//      } 
//    }
//  }
//}