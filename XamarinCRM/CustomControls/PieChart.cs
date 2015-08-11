using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.CustomControls
{
  public class PieChart : View
  {
    //Bindable property for the items
    public static readonly BindableProperty ItemsProperty =
      BindableProperty.Create<PieChart, List<BarItem>>(p => p.Items, new List<BarItem>());
    //Gets or sets the color of the items
    public List<BarItem> Items
    {
      get { return (List<BarItem>)GetValue(ItemsProperty); }
      set { SetValue(ItemsProperty, value); }
    }
  }
}
