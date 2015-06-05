using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MobileCRM.Shared.ViewModels.Accounts;
using Xamarin;

namespace MobileCRM.Shared.Pages.Accounts
{
    public class AccountMapView : BaseView
    {
        public AccountDetailsViewModel ViewModel
        {
            get { return BindingContext as AccountDetailsViewModel; }
        }

        Map map;

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

        public async void MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.LoadPin();
            Pin pin = await getPinTask;

            map.Pins.Clear();
            map.Pins.Add(pin);
            
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MakeMap();

            Insights.Track("Account Contact Map Page");
        }
    }
}