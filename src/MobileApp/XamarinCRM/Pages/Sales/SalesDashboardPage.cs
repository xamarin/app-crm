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
using XamarinCRM.Pages.Splash;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Services;
using XamarinCRM.Models;
using XamarinCRM.Views;

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
            SalesDashboardChartView salesChartView = null;
            _SalesDashboardChartViewModel = new SalesDashboardChartViewModel();
            try
            {
                salesChartView = new SalesDashboardChartView() { BindingContext = _SalesDashboardChartViewModel };    
            }
            catch (Exception ex)
            {
                
            }

            #endregion

            #region leads view
            var newLeadCommand = new Command(PushTabbedLeadPageAction);
            _SalesDashboardLeadsViewModel = new SalesDashboardLeadsViewModel(newLeadCommand);
            var leadsView = new LeadsView { BindingContext = _SalesDashboardLeadsViewModel };
            #endregion

            scrollView = new ScrollView
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
                ToolbarItems.Add(new ToolbarItem("Add", "add_ios_gray", () =>
                        {
                            _SalesDashboardLeadsViewModel.PushLeadDetailsTabbedPageCommand.Execute(null);
                        }));

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


            Page page = null;
            var leadDetail = new LeadDetailPage()
            {
                BindingContext = viewModel,
                Title = TextResources.Details,
                Icon = new FileImageSource() { File = "LeadDetailTab" } // only used on iOS
            };
            
            if (Device.OS == TargetPlatform.iOS)
            {
                page = leadDetail;
            }
            else
            {
                page = new TabbedPage();
                ((TabbedPage)page).Children.Add(leadDetail);

                ((TabbedPage)page).Children.Add(new LeadContactDetailPage()
                    {
                        BindingContext = viewModel,
                        Title = TextResources.Contact,
                        Icon = new FileImageSource() { File = "LeadContactDetailTab" } // only used on iOS
                    });
                
            }


            if (lead != null)
            {
                page.Title = lead.Company;
            }
            else
            {
                page.Title = "New Lead";
            }

            page.ToolbarItems.Add(
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

           
            await Navigation.PushAsync(page);
        }
    }
}
