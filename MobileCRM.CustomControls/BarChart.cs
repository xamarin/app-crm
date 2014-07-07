using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.CustomControls
{
    public class BarItem
    {
      public float Value { get; set; }
      public string Name { get; set; }
    }

    public class BarChart : View
    {
      //Bindable property for the items
      public static readonly BindableProperty ItemsProperty =
        BindableProperty.Create<BarChart, List<BarItem>>(p => p.Items, new List<BarItem>());
      //Gets or sets the color of the items
      public List<BarItem> Items
      {
        get { return (List<BarItem>)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
      }
    }
}
