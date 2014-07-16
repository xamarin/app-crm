using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using MobileCRM.Shared.ViewModels.Accounts;

namespace MobileCRM.Shared.Pages.Accounts
{
    public class AccountMapView : BaseView
    {
        public AccountDetailsViewModel ViewModel
        {
            get { return BindingContext as AccountDetailsViewModel; }
        }

        private Map map;
        public AccountMapView(AccountDetailsViewModel vm)
        {
            this.Title = "Map";
            this.Icon = "map.png";

            this.BindingContext = vm;


            ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Account")
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
            Pin pin = ViewModel.LoadPin();

            map.Pins.Clear();
            map.Pins.Add(pin);
            
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            return map;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MakeMap();

        }
    }
}
