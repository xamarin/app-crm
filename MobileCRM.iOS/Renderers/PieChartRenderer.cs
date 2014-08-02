using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MobileCRM.iOS.Renderers;
using System.ComponentModel;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.XamarinIOS;
[assembly: ExportRenderer(typeof(MobileCRM.Shared.CustomControls.PieChart), typeof(PieChartRenderer))]
namespace MobileCRM.iOS.Renderers
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
      var pieSeries1 = new PieSeries();
      pieSeries1.InsideLabelPosition = 0.7;
      pieSeries1.StrokeThickness = 2;
      plotModel1.Series.Add(pieSeries1);

      pieSeries1.InsideLabelColor = OxyColors.White;

      foreach(var item in Element.Items)
      {
       
       pieSeries1.Slices.Add(new PieSlice
         {
           Label = item.Name,
           Value = item.Value
         });
      }

      var plotView = new PlotView();


      //Add padding to prevent cropping
      plotModel1.Padding = new OxyPlot.OxyThickness(45);

      //plotModel1.Title = "Sales by Category";

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