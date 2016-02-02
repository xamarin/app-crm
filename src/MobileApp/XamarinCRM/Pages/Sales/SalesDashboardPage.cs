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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Models;
using XamarinCRM.Pages.Splash;
using XamarinCRM.Services;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Views;

namespace XamarinCRM.Pages.Sales
{
    public class SalesDashboardPage : ContentPage
    {
        IAuthenticationService _AuthenticationService;
        ScrollView _ScrollView;
        FloatingActionButtonView _Fab;
        // We're holding on to these ViewModel properties because a couple of child views are reliant on these ViewModels, as well as the OnAppearing()
        // method in this Page needing access to some of the public methods on those ViewModels, e.g. ExecuteLoadSeedDataCommand().
        SalesDashboardChartViewModel _SalesDashboardChartViewModel { get; set; }

        SalesDashboardLeadsViewModel _SalesDashboardLeadsViewModel { get; set; }

        public SalesDashboardPage()
        {
            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            this.SetBinding(Page.TitleProperty, new Binding() { Source = TextResources.Sales });

            #region sales chart view
            var salesChartView = new SalesDashboardChartView() { BindingContext = _SalesDashboardChartViewModel = new SalesDashboardChartViewModel() };

            #endregion

            #region leads view
            var leadsView = new LeadsView { BindingContext = _SalesDashboardLeadsViewModel = new SalesDashboardLeadsViewModel(new Command(PushTabbedLeadPageAction)) };
            #endregion

            _ScrollView = new ScrollView
            { 
                Content = new StackLayout
                {
                    Spacing = 0,
                    Children =
                    {
                        salesChartView,
                        leadsView
                    }
                }
            };
                        
            #region compose view hierarchy
            if (Device.OS == TargetPlatform.Android)
            {
                _Fab = new FloatingActionButtonView
                {
                    ImageName = "fab_add.png",
                    ColorNormal = Palette._001,
                    ColorPressed = Palette._002,
                    ColorRipple = Palette._001,
                    Clicked = (sender, args) => 
                            _SalesDashboardLeadsViewModel.PushTabbedLeadPageCommand.Execute(null),
                };

                var absolute = new AbsoluteLayout
                { 
                    VerticalOptions = LayoutOptions.FillAndExpand, 
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                // Position the pageLayout to fill the entire screen.
                // Manage positioning of child elements on the page by editing the pageLayout.
                AbsoluteLayout.SetLayoutFlags(_ScrollView, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(_ScrollView, new Rectangle(0f, 0f, 1f, 1f));
                absolute.Children.Add(_ScrollView);

                // Overlay the FAB in the bottom-right corner
                AbsoluteLayout.SetLayoutFlags(_Fab, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(_Fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                absolute.Children.Add(_Fab);

                Content = absolute;
            }
            else
            {
                ToolbarItems.Add(new ToolbarItem("Add", "add_ios_gray", () =>
                    _SalesDashboardLeadsViewModel.PushTabbedLeadPageCommand.Execute(null)));

                Content = _ScrollView;
            }
            #endregion

            #region wire up MessagingCenter
            // Catch the login success message from the MessagingCenter.
            // This is really only here for Android, which doesn't fire the OnAppearing() method in the same way that iOS does (every time the page appears on screen).
            Device.OnPlatform(Android: () => MessagingCenter.Subscribe<SplashPage>(this, MessagingServiceConstants.AUTHENTICATED, sender => OnAppearing()));
            #endregion
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // don't show any content until we're authenticated
            if (_AuthenticationService.IsAuthenticated)
            {
                Content.IsVisible = true;

                if (!_SalesDashboardChartViewModel.IsInitialized) 
                {
                    await _SalesDashboardChartViewModel.ExecuteLoadSeedDataCommand();
                    _SalesDashboardChartViewModel.IsInitialized = true;
                }

                if (!_SalesDashboardLeadsViewModel.IsInitialized) 
                {
                    await _SalesDashboardLeadsViewModel.ExecuteLoadSeedDataCommand();
                    _SalesDashboardLeadsViewModel.IsInitialized = true;
                }

                Insights.Track(InsightsReportingConstants.PAGE_SALESDASHBOARD);
            }
            else
            {
                Content.IsVisible = false;
            }
        }

        Action<object> PushTabbedLeadPageAction
        {
            get { return new Action<object>(o => PushTabbedLeadPage((Account)o)); }
        }

        async Task PushTabbedLeadPage(Account lead = null)
        {
            LeadDetailViewModel viewModel = new LeadDetailViewModel(Navigation, lead); 

            var leadDetailPage = new LeadDetailPage()
            {
                BindingContext = viewModel,
                Title = TextResources.Details,
                
            };
            if (Device.OS == TargetPlatform.iOS)
                leadDetailPage.Icon = Icon = new FileImageSource() { File = "LeadDetailTab" };


            if (lead != null)
            {
                leadDetailPage.Title = lead.Company;
            }
            else
            {
                leadDetailPage.Title = "New Lead";
            }

            leadDetailPage.ToolbarItems.Add(
                new ToolbarItem(TextResources.Save, "save.png", async () =>
                    {

                        if (string.IsNullOrWhiteSpace(viewModel.Lead.Company))
                        {
                            await DisplayAlert("Missing Information", "Please fill in the lead's company to continue", "OK");
                            return;
                        }

                        var answer = 
                            await DisplayAlert(
                                title: TextResources.Leads_SaveConfirmTitle,
                                message: TextResources.Leads_SaveConfirmDescription,
                                accept: TextResources.Save,
                                cancel: TextResources.Cancel);

                        if (answer)
                        {
                            viewModel.SaveLeadCommand.Execute(null);

                            await Navigation.PopAsync();
                        }
                    }));

           
            await Navigation.PushAsync(leadDetailPage);
        }
    }
}
