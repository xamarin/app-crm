//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using ExternalMaps.Plugin;
using ExternalMaps.Plugin.Abstractions;
using XamarinCRM.Statics;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerMapPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        Map _Map;

        public CustomerMapPage(CustomerDetailViewModel viewModel)
        {
            this.BindingContext = viewModel;

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

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERMAP);
        }
    }
}

