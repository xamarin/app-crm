using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace MobileCRM.Shared.Pages.Contacts
{
    public class ContactsMapView : BaseView
    {
      public ContactsViewModel ViewModel
      {
        get { return BindingContext as ContactsViewModel; }
      }

      private Map map;
      public ContactsMapView(ContactsViewModel vm)
      {
        this.Title = "Map";
        this.Icon = "map.png";

        this.BindingContext = vm;


        ViewModel.PropertyChanged += (sender, args) =>
          {
            if (args.PropertyName == "Contacts")
              MakeMap();
          };
       

        map = new Map()
        {
          IsShowingUser = true
        };

        MakeMap();
        var stack = new StackLayout { Spacing = 0 };

        map.VerticalOptions = LayoutOptions.FillAndExpand;
        map.HeightRequest = 100;
        map.WidthRequest = 960;

        stack.Children.Add(map);
        Content = stack;
      }

      public Map MakeMap()
      {

        var pins = ViewModel.LoadPins();

        map.Pins.Clear();


        if(pins.Count > 0)
        {
          foreach (var p in pins)
          {
            map.Pins.Add(p);
          }
          
          map.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(0.3)));
        }

        return map;
      }
      protected override void OnAppearing()
      {
        base.OnAppearing();
        MakeMap();
        
      }
    }
}
