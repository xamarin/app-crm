using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Interfaces;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerMapPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        Map _Map;

        INativeDirectionsPresenter _NativeDirectionsPresenter;

        public CustomerMapPage(CustomerDetailViewModel viewModel)
        {
            this.BindingContext = viewModel;

            _NativeDirectionsPresenter = DependencyService.Get<INativeDirectionsPresenter>();

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
                            _NativeDirectionsPresenter.PresentDirections(await ViewModel.LoadPin());
                            await ViewModel.PopModalAsync();
                        }
                    }
                )
            );

            ToolbarItems.Add(
                new ToolbarItem(
                    TextResources.Close, 
                    null, 
                    async () => await ViewModel.PopModalAsync()));
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

            Insights.Track("Contact Map Page");
        }
    }
}

