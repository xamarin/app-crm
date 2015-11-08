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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Splash;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Services;
using XamarinCRM.Models;
using XamarinCRM.Views.Custom;

namespace XamarinCRM.Pages.Sales
{
    public class SalesDashboardPage : ContentPage
    {
        IAuthenticationService _AuthenticationService;
        ScrollView scrollView;
        FloatingActionButtonView fab;
        // We're holding on to these ViewModel properties because a couple of child views are reliant on these ViewModels, as well as the OnAppearing()
        // method in this Page needing access to some of the public methods on those ViewModels, e.g. ExecuteLoadSeedDataCommand().
        SalesDashboardChartViewModel _SalesDashboardChartViewModel { get; set; }

        SalesDashboardLeadsViewModel _SalesDashboardLeadsViewModel { get; set; }

        public SalesDashboardPage()
        {
            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            this.SetBinding(Page.TitleProperty, new Binding() { Source = TextResources.Sales });

            #region sales chart view
            _SalesDashboardChartViewModel = new SalesDashboardChartViewModel();
            SalesDashboardChartView salesChartView = new SalesDashboardChartView() { BindingContext = _SalesDashboardChartViewModel };
            #endregion

            #region leads view
            var newLeadCommand = new Command(PushTabbedLeadPageAction);
            _SalesDashboardLeadsViewModel = new SalesDashboardLeadsViewModel(newLeadCommand);
            var leadsView = new LeadsView { BindingContext = _SalesDashboardLeadsViewModel };
            #endregion

            scrollView = new ScrollView
            { 
                Content = new UnspacedStackLayout
                {
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
                fab = new FloatingActionButtonView
                {
                    ImageName = "fab_add.png",
                    ColorNormal = Palette._001,
                    ColorPressed = Palette._002,
                    ColorRipple = Palette._001,
                    Clicked = (sender, args) => 
                            newLeadCommand.Execute(null),
                };

                var absolute = new AbsoluteLayout
                { 
                    VerticalOptions = LayoutOptions.FillAndExpand, 
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                // Position the pageLayout to fill the entire screen.
                // Manage positioning of child elements on the page by editing the pageLayout.
                AbsoluteLayout.SetLayoutFlags(scrollView, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(scrollView, new Rectangle(0f, 0f, 1f, 1f));
                absolute.Children.Add(scrollView);

                // Overlay the FAB in the bottom-right corner
                AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                absolute.Children.Add(fab);

                Content = absolute;

            }
            else
            {
                Content = scrollView;
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

                var tasksToRun = new List<Task>()
                { 
                    Task.Factory.StartNew(async () =>
                        {
                            if (!_SalesDashboardChartViewModel.IsInitialized)
                            {
                                await _SalesDashboardChartViewModel.ExecuteLoadSeedDataCommand();
                                _SalesDashboardChartViewModel.IsInitialized = true;
                            }
                        }),
                    Task.Factory.StartNew(async () =>
                        {
                            if (!_SalesDashboardLeadsViewModel.IsInitialized)
                            {
                                await _SalesDashboardLeadsViewModel.ExecuteLoadSeedDataCommand();
                                _SalesDashboardLeadsViewModel.IsInitialized = true;
                            }
                        })
                };

                // Awaiting these parallel task allows the leadsView and salesChartView to load independently.
                // Task.WhenAll() is your friend in cases like these, where you want to load from two different data models on a single page.
                await Task.WhenAll(tasksToRun.ToArray());

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

            TabbedPage tabbedPage = new TabbedPage();

            tabbedPage.ToolbarItems.Add(
                new ToolbarItem(TextResources.Save, "save.png", async () =>
                    {
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

            tabbedPage.Children.Add(new LeadDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Details,
                    Icon = new FileImageSource() { File = "LeadDetailTab" } // only used on iOS
                });

            tabbedPage.Children.Add(new LeadContactDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" } // only used on iOS
                });

            await Navigation.PushAsync(tabbedPage);
        }
    }
}
