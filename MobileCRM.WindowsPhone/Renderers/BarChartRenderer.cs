using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using MobileCRM.WindowsPhone.Renderers;
using System.ComponentModel;
using System.Windows.Controls.DataVisualization.Charting;
[assembly: ExportRenderer(typeof(MobileCRM.CustomControls.BarChart), typeof(BarChartRenderer))]
namespace MobileCRM.WindowsPhone.Renderers
{

  public class BarChartRenderer : ViewRenderer<MobileCRM.CustomControls.BarChart, Chart>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomControls.BarChart> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;
      var barChart = new Chart();
      barChart.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
      var series = new ColumnSeries();
      series.Title = string.Empty;
      series.ItemsSource = this.Element.Items;
      series.IndependentValueBinding = new System.Windows.Data.Binding("Name");
      series.DependentValueBinding = new System.Windows.Data.Binding("Value");
      barChart.Series.Add(series);

      SetNativeControl(barChart);
    }
    

    protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e) {
      base.OnElementPropertyChanged(sender, e);
      if (this.Element == null || this.Control == null)
        return;
      if (e.PropertyName == MobileCRM.CustomControls.BarChart.ItemsProperty.PropertyName)
      {
        this.Control.Series.Clear();
        var series = new ColumnSeries();
        series.Title = string.Empty;
        series.ItemsSource = this.Element.Items;
        series.IndependentValueBinding = new System.Windows.Data.Binding("Name");
        series.DependentValueBinding = new System.Windows.Data.Binding("Value");
        this.Control.Series.Add(series);
      } 
    }
  }
}