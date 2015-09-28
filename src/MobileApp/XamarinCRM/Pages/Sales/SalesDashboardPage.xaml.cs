using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Models;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Sales;

namespace XamarinCRM.Pages.Sales
{
    public partial class SalesDashboardPage : ContentPage
    {
        IAuthenticationService _AuthenticationService;

        SalesDashboardChartViewModel _SalesDashboardChartViewModel { get; set; }

        SalesDashboardLeadsViewModel _SalesDashboardLeadsViewModel { get; set; }

        public SalesDashboardPage()
        {
            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // don't show any content until we're authenticated

            salesChartView.BindingContext = _SalesDashboardChartViewModel;

            Content.IsVisible = false;

            if (_AuthenticationService.IsAuthenticated)
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
                    Icon = new FileImageSource() { File = "LeadDetailTab" } // only used on iOS
                });

            tabbedPage.Children.Add(new LeadContactDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" } // only used on iOS
                });

            NavigationPage navPage = new NavigationPage(tabbedPage);

            await Navigation.PushModalAsync(navPage);
        }
    }
}

