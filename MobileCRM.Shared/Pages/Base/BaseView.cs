using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages
{
  public class BaseView : ContentPage
  {
    public BaseView()
    {
      SetBinding(Page.TitleProperty, new Binding("Title"));
      SetBinding(Page.IconProperty, new Binding("Icon"));
    }
  }
}
