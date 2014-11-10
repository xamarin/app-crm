using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using MobileCRM.WindowsPhone.Renderers;
using MobileCRM.Shared.Helpers;
using System.ComponentModel;
using OxyPlot.WP8;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


[assembly: ExportRenderer(typeof(MobileCRM.Shared.CustomControls.BarChart), typeof(BarChartRenderer))]
namespace MobileCRM.WindowsPhone.Renderers
{

  public class BarChartRenderer : ViewRenderer<MobileCRM.Shared.CustomControls.BarChart, PlotView>
  {
      private static OxyColor FILLCOLOR = OxyColor.FromRgb(180, 188, 188);

    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.Shared.CustomControls.BarChart> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;

      var plotModel1 = new PlotModel();

      plotModel1.Background = OxyColors.Transparent;


      plotModel1.LegendBorderThickness = 0;
      plotModel1.LegendOrientation = LegendOrientation.Horizontal;
      plotModel1.LegendPlacement = LegendPlacement.Outside;
      plotModel1.LegendPosition = LegendPosition.BottomCenter;
      plotModel1.PlotAreaBorderColor = OxyColors.White;
      

      var categoryAxis1 = new CategoryAxis();
      categoryAxis1.AxislineColor = OxyColors.White;
      categoryAxis1.MajorGridlineColor = OxyColors.White;
      categoryAxis1.MinorGridlineColor = OxyColors.White;
      categoryAxis1.TextColor = OxyColors.White;
      categoryAxis1.ExtraGridlineColor = OxyColors.White;
      plotModel1.Axes.Add(categoryAxis1);

      var linearAxis1 = new LinearAxis();
      linearAxis1.AxislineColor = OxyColors.White;
      linearAxis1.MajorGridlineColor = OxyColors.White;
      linearAxis1.MinorGridlineColor = OxyColors.White;
      linearAxis1.TextColor = OxyColors.White;
      linearAxis1.ExtraGridlineColor = OxyColors.White;
      linearAxis1.AbsoluteMinimum = 0;
      linearAxis1.MaximumPadding = 0.06;
      linearAxis1.MinimumPadding = 0;
      plotModel1.Axes.Add(linearAxis1);

      var columnSeries1 = new ColumnSeries();
      columnSeries1.LabelFormatString = "{0}";
      columnSeries1.LabelPlacement = LabelPlacement.Inside;
      columnSeries1.StrokeThickness = 0;
      columnSeries1.TextColor = OxyColors.White;
      columnSeries1.StrokeColor = OxyColors.Transparent;
      plotModel1.Series.Add(columnSeries1);

      foreach (var item in Element.Items)
      {

        columnSeries1.Items.Add(new ColumnItem(item.Value, -1) { Color = FILLCOLOR });
        categoryAxis1.ActualLabels.Add(item.Name);
      }

      var plotView = new PlotView();
      
      plotView.Model = plotModel1;
     
      SetNativeControl(plotView);
    }
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);
      if (this.Element == null || this.Control == null)
        return;
      if (e.PropertyName == MobileCRM.Shared.CustomControls.BarChart.ItemsProperty.PropertyName)
      {
        var columnSeries1 = Control.Model.Series[0] as ColumnSeries;
        var categoryAxis1 = Control.Model.Axes[0] as CategoryAxis;

        columnSeries1.Items.Clear();
        categoryAxis1.Labels.Clear();
        categoryAxis1.ActualLabels.Clear();
        foreach (var item in Element.Items)
        {

          //columnSeries1.Items.Add(new ColumnItem(item.Value));
          columnSeries1.Items.Add(new ColumnItem(item.Value, -1) { Color = FILLCOLOR });
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