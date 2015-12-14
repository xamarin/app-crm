// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERMAP);
        }
    }
}

