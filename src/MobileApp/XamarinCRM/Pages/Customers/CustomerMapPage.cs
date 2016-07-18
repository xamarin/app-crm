
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Statics;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerMapPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        Map _Map;

        public CustomerMapPage()
        {
            _Map = new Map()
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ToolbarItems.Add(
                new ToolbarItem(
                    TextResources.Get_Directions, 
                    null, 
                    async () =>
                    {
                        if (await DisplayAlert(
                                TextResources.Leave_Application, 
                                TextResources.Customers_Detail_MappingDirections_LeaveApplication, 
                                TextResources.Customers_Detail_MappingDirections_LeaveApplication_Yes, 
                                TextResources.Cancel))
                        {

                            var pin = await ViewModel.LoadPin();

                            CrossExternalMaps.Current.NavigateTo(pin.Label, pin.Position.Latitude, pin.Position.Longitude, NavigationType.Driving);

                            await ViewModel.PopAsync();
                        }
                    }
                )
            );

        }

        public async Task MakeMap()
        {
            Task<Pin> getPinTask = ViewModel.LoadPin();

            Pin pin = await getPinTask;

            _Map.Pins.Clear();

            _Map.Pins.Add(pin);

            _Map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: _Map,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            Content = relativeLayout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await this.MakeMap();
        }
    }
}

