using System.Collections.Generic;
using MobileCRM.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MobileCRM.ViewModels.Accounts;
using Xamarin;

namespace MobileCRM.Pages.Accounts
{
    public class AccountsMapView : BaseView
    {
        public AccountsViewModel ViewModel
        {
            get { return BindingContext as AccountsViewModel; }
        }

        private Map map;

        public AccountsMapView(AccountsViewModel vm)
        {
            this.Title = "Map";
            this.Icon = "map.png";

            this.BindingContext = vm;

            ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Accounts")
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

            if (pins.Count > 0)
            {
                foreach (var p in pins)
                {
                    map.Pins.Add(p);
                }
          
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(5)));
            }

            return map;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MakeMap();

            Insights.Track("Account Map View Page");
        }
    }
}