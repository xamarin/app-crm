using MobileCRM.ViewModels.Contacts;
using System.Threading.Tasks;
using MobileCRM.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin;

namespace MobileCRM.Pages.Contacts
{
    public class ContactMapView : BaseView
    {
        public ContactDetailsViewModel ViewModel
        {
            get { return BindingContext as ContactDetailsViewModel; }
        }

        Map map;

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

            Insights.Track("Contact Map Page");
        }
    }
}