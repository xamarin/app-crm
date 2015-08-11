using System.Threading.Tasks;
using XamarinCRM.Cells;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Views.Sales;
using Xamarin;
using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Base;
using Syncfusion.SfChart.XForms;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Splash;

namespace XamarinCRM.Pages.Sales
{
    public class SalesDashboardPage : ModelTypedContentPage<SalesDashboardViewModel>
    {
        /// <summary>
        /// A necessary flag for dealing with the async Auth UI. If we don't use this, then the Auth UI gets presented twice, even when the first login attempt is successful.
        /// This is because this Page's OnAppearing() method gets called before the Auth UI returns its result.
        /// </summary>
        //        bool _DidPresentAuthUI;

        public SalesDashboardPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            SetBinding(TitleProperty, new Binding() { Source = TextResources.Sales });

            #region sales graph header
            SalesChartHeaderView chartHeaderView = new SalesChartHeaderView();
            chartHeaderView.WeeklyAverageValueLabel.SetBinding(Label.TextProperty, "SalesAverage");
            #endregion

            #region the sales graph
            double chartHeight = Device.OnPlatform(190, 190, 180);

            StackLayout salesChartStackLayout = new UnspacedStackLayout() { HeightRequest = chartHeight };
            Device.OnPlatform(iOS: () => salesChartStackLayout.BackgroundColor = Color.Transparent, Android: () => salesChartStackLayout.BackgroundColor = Palette._009);

            ActivityIndicator chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.MediumRowHeight
            };
            chartActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            ColumnSeries columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._010 },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._011 },
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._010 }
                },
                Color = Palette._004
            };

            // Not currently working because a binding bug in the SyncFusion ColumnSeries.ItemsSourceProperty setter
            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, "SalesChartDataPoints");

            SfChart chart = new SfChart()
            {
                HeightRequest = chartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._010 }
                }
            };
            Device.OnPlatform(
                iOS: () =>
                {
                    chart.BackgroundColor = Color.Transparent;
                    salesChartStackLayout.Padding = new Thickness(0, 20, 30, 0);
                }, 
                Android: () => chart.BackgroundColor = Palette._009);

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsModelLoaded");
            chart.SetBinding(IsVisibleProperty, "IsModelLoaded");

            salesChartStackLayout.Children.Add(chartActivityIndicator);
            salesChartStackLayout.Children.Add(chart);
            #endregion

            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            LeadListHeaderView leadListHeaderView = new LeadListHeaderView(async () => await PushTabbedPage());
            leadListHeaderView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(IsVisibleProperty, "IsModelLoaded");
            #endregion

            #region leads list activity inidicator
            ActivityIndicator leadListActivityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.MediumRowHeight
            };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region leadsListView
            LeadListView leadListView = new LeadListView();
            leadListView.SetBinding(LeadListView.ItemsSourceProperty, "Leads");
            leadListView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListView.SetBinding(IsVisibleProperty, "IsModelLoaded");

            leadListView.ItemTapped += async (sender, e) =>
            {
                Account leadListItem = (Account)e.Item;
                await PushTabbedPage(leadListItem);
            };
            #endregion

            #region setup stack layout and add children
            // Instantiate a StackLayout that several view elements will be added to.
            StackLayout stackLayout = new UnspacedStackLayout();

            // conditionally set the top padding to 20px to account for the iOS status bar
            Device.OnPlatform(iOS: () => stackLayout.Padding = new Thickness(0, 20, 0, 0));

            stackLayout.Children.Add(chartHeaderView);

            stackLayout.Children.Add(salesChartStackLayout);

            stackLayout.Children.Add(leadListHeaderView);

            stackLayout.Children.Add(leadListActivityIndicator);

            stackLayout.Children.Add(leadListView);
            #endregion

            // assign the built-up stack layout to the Content property of this page
            Content = new ScrollView() { Content = stackLayout };

            Content.IsVisible = false;

            // Catch the login success message from the MessagingCenter.
            // This is really only here for Android, which doesn't fire the OnAppearing() method in the same way that iOS does (every time the page appears on screen).
            Device.OnPlatform(Android: () =>
                {
                    MessagingCenter.Subscribe<SplashPage>(this, MessagingServiceConstants.AUTHENTICATED, sender => OnAppearing());
                });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Content.IsVisible = false;

            if (App.IsAuthenticated)
            {
                Content.IsVisible = true;

                await ViewModel.ExecuteLoadSeedDataCommand();

                ViewModel.IsInitialized = true;

                Insights.Track("Dashboard Page");
            }
        }

        async Task PushTabbedPage(Account lead = null)
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
                
                            await ViewModel.PopModalAsync();
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
                                await ViewModel.PopModalAsync();
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

            await ViewModel.PushModalAsync(navPage);
        }
    }
}

