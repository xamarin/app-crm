using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using MobileCRM.Shared.ViewModels.Contacts;


namespace MobileCRM.Shared.Pages.Contacts
{
    public class ContactMapView : BaseView
    {
        public ContactDetailsViewModel ViewModel
        {
            get { return BindingContext as ContactDetailsViewModel; }
        }

        private Map map;
        public ContactMapView(ContactDetailsViewModel vm)
        {
            this.Title = "Map";
            this.Icon = "map.png";

            this.BindingContext = vm;


            ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Contact")
                    MakeMap();
            };


            map = new Map()
            {
                IsShowingUser = true
            };

            this.MakeMap();

            var stack = new StackLayout { Spacing = 0 };

            map.VerticalOptions = LayoutOptions.FillAndExpand;
            map.HeightRequest = 100;
            map.WidthRequest = 960;

            stack.Children.Add(map);
            Content = stack;
        }

        public async void MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.LoadPin();

            Pin pin = await getPinTask;

            map.Pins.Clear();
            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            this.MakeMap();

        }
    }
}
