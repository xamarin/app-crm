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
[assembly: ExportRenderer(typeof(MobileCRM.CustomViews.BarChart), typeof(BarChartRenderer))]
namespace MobileCRMAndroid.Renderers
{

  public class BarChartRenderer : ViewRenderer<MobileCRM.CustomViews.BarChart, BarChart.BarChartView>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomViews.BarChart> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;
      var barChart = new BarChart.BarChartView(Forms.Context)
      {
        ItemsSource = Element.Items.Select(item => new BarChart.BarModel
        {
          Value = item.Value,
          Legend = item.Name
        })
      };
      SetNativeControl(barChart);
    }
    protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e) {
      base.OnElementPropertyChanged(sender, e);
      if (this.Element == null || this.Control == null)
        return;
      if (e.PropertyName == MobileCRM.CustomViews.BarChart.ItemsProperty.PropertyName)
      {
        Control.ItemsSource = Element.Items.Select(item => new BarChart.BarModel
        {
          Value = item.Value,
          Legend = item.Name
        });
      } 
    }
  }
}