using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using System.ComponentModel;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.WP8;
using MobileCRM.WindowsPhone.Renderers;
[assembly: ExportRenderer(typeof(MobileCRM.Shared.CustomControls.PieChart), typeof(PieChartRenderer))]
namespace MobileCRM.WindowsPhone.Renderers
{

  public class PieChartRenderer : ViewRenderer<MobileCRM.Shared.CustomControls.PieChart, PlotView>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.Shared.CustomControls.PieChart> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;

      var plotModel1 = new PlotModel();
      plotModel1.Title = string.Empty;
      plotModel1.TextColor = OxyColors.White;
      var pieSeries1 = new PieSeries();
      pieSeries1.InsideLabelPosition = 0.8;
      pieSeries1.StrokeThickness = 2;
      plotModel1.Series.Add(pieSeries1);

      foreach(var item in Element.Items)
      {
       
       pieSeries1.Slices.Add(new PieSlice
         {
           Label = item.Name,
           Value = item.Value
         });
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

        var pieSeries1 = Control.Model.Series[0] as PieSeries;
        pieSeries1.Slices.Clear();
        foreach(var item in Element.Items)
        {
       
         pieSeries1.Slices.Add(new PieSlice
           {
             Label = item.Name,
             Value = item.Value
           });
        }

        Control.Model.InvalidatePlot(true);
        Control.InvalidatePlot(true);
       
      }
    }
  }
}