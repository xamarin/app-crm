using System.ComponentModel;
using MobileCRM.CustomControls;
using MobileCRM.WindowsPhone.Renderers;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WP8;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(PieChart), typeof(PieChartRenderer))]
namespace MobileCRM.WindowsPhone.Renderers
{

  public class PieChartRenderer : ViewRenderer<CustomControls.PieChart, PlotView>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<CustomControls.PieChart> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || Element == null)
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
      if (Element == null || Control == null)
        return;
      if (e.PropertyName == CustomControls.BarChart.ItemsProperty.PropertyName)
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