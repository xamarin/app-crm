using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Pages.Splash;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;

namespace XamarinCRM.Pages.Sales
{
    public class SalesDashboardPage : ContentPage
    {
        // We're holding on to theese ViewModel properties because a couple of child views are reliant on these ViewModels, as well as the OnAppearing()
        // method in this Page needing access to some of the public methods on those ViewModels, e.g. ExecuteLoadSeedDataCommand().
        SalesDashboardChartViewModel _SalesDashboardChartViewModel { get; set; }

        SalesDashboardLeadsViewModel _SalesDashboardLeadsViewModel { get; set; }

        public SalesDashboardPage()
        {
            // If this page is being presented by a NavigationPage, we don't want to show the navigation bar (top) in this particular app design.
            NavigationPage.SetHasNavigationBar(this, false);

            this.SetBinding(ContentPage.TitleProperty, new Binding() { Source = TextResources.Sales });

            #region sales chart view
            _SalesDashboardChartViewModel = new SalesDashboardChartViewModel();
            SalesChartView salesChartView = new SalesChartView() { BindingContext = _SalesDashboardChartViewModel };
            #endregion

            #region leads view
            _SalesDashboardLeadsViewModel = new SalesDashboardLeadsViewModel(new Command(new Action<object>(o => PushTabbedLeadPage((Account)o))));
            LeadsView leadsView = new LeadsView() { BindingContext = _SalesDashboardLeadsViewModel };
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout();

            // conditionally set the top padding to 20px to account for the iOS status bar
            // Device.OnPlatform(iOS: () => stackLayout.Padding = new Thickness(0, 20, 0, 0));

            stackLayout.Children.Add(salesChartView);
            stackLayout.Children.Add(leadsView);
            #endregion

            // assign the built-up stack layout to the Content property of this page
            Content = new ScrollView() { Content = stackLayout };

            Content.IsVisible = false;

            // Catch the login success message from the MessagingCenter.
            // This is really only here for Android, which doesn't fire the OnAppearing() method in the same way that iOS does (every time the page appears on screen).
            Device.OnPlatform(Android: () => MessagingCenter.Subscribe<SplashPage>(this, MessagingServiceConstants.AUTHENTICATED, sender => OnAppearing()));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // don't show any content until we're authenticated

            Content.IsVisible = false;

            if (App.IsAuthenticated)
            {
                Content.IsVisible = true;

                List<Task> tasksToRun = new List<Task>()
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

                Insights.Track("Dashboard Page");
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
                new ToolbarItem(TextResources.Save, null, async () =>
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

                            await Navigation.PopModalAsync();
                        }
                    }));

            tabbedPage.ToolbarItems.Add(
                new ToolbarItem(TextResources.Exit, null, async () =>
                    {
                        {
                            var answer = 
                                await DisplayAlert(
                                    title: TextResources.Leads_ExitConfirmTitle,
                                    message: TextResources.Leads_ExitConfirmDescription,
                                    accept: TextResources.Exit_and_Discard,
                                    cancel: TextResources.Cancel);

                            if (answer)
                            {
                                await Navigation.PopModalAsync();
                            }
                        }
                    }));

            tabbedPage.Children.Add(new LeadDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Details,
                    Icon = new FileImageSource() { File = "LeadDetailTab" }
                });

            tabbedPage.Children.Add(new LeadContactDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" }
                });

            NavigationPage navPage = new NavigationPage(tabbedPage);

            await Navigation.PushModalAsync(navPage);
        }
    }
}

