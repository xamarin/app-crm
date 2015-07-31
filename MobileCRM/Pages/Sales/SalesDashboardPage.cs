using System.Threading.Tasks;
using MobileCRM.Cells;
using MobileCRM.Models;
using MobileCRM.ViewModels.Sales;
using MobileCRM.Views.Sales;
using Xamarin;
using Xamarin.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MobileCRM.Layouts;
using MobileCRM.Pages.Base;
using Syncfusion.SfChart.XForms;

namespace MobileCRM.Pages.Sales
{
    public class SalesDashboardPage : ModelEnforcedContentPage<SalesDashboardViewModel>
    {
        public IPlatformParameters PlatformParameters { get; set; }

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
            Device.OnPlatform(iOS: () => salesChartStackLayout.BackgroundColor = Color.Transparent, Android: () => salesChartStackLayout.BackgroundColor = Palette._008);

            ActivityIndicator chartActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.MediumRowHeight
            };
            chartActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            chartActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            chartActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");

            ColumnSeries columnSeries = new ColumnSeries()
            {
                YAxis = new NumericalAxis()
                {
                    OpposedPosition = false,
                    ShowMajorGridLines = true,
                    MajorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._009 },
                    ShowMinorGridLines = true,
                    MinorTicksPerInterval = 1,
                    MinorGridLineStyle = new ChartLineStyle() { StrokeColor = Palette._010 },
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._009 }
                },
                Color = Palette._004
            };

            // Not currently working because a binding bug in the SyncFusion ColumnSeries.ItemsSourceProperty setter
            columnSeries.SetBinding(ColumnSeries.ItemsSourceProperty, new Binding("SalesChartDataPoints"));

            SfChart chart = new SfChart()
            {
                HeightRequest = chartHeight,

                PrimaryAxis = new CategoryAxis()
                {
                    EdgeLabelsDrawingMode = EdgeLabelsDrawingMode.Center,
                    LabelPlacement = LabelPlacement.BetweenTicks,
                    TickPosition = AxisElementPosition.Inside,
                    ShowMajorGridLines = false,
                    LabelStyle = new ChartAxisLabelStyle() { TextColor = Palette._009 }
                }
            };
            Device.OnPlatform(
                iOS: () =>
                {
                    chart.BackgroundColor = Color.Transparent;
                    salesChartStackLayout.Padding = new Thickness(0, 20, 30, 0);
                }, 
                Android: () => chart.BackgroundColor = Palette._008);

            chart.Series.Add(columnSeries);
            chart.SetBinding(IsEnabledProperty, "IsModelLoaded");
            chart.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            chart.SetBinding(IsVisibleProperty, "IsModelLoaded");

            salesChartStackLayout.Children.Add(chartActivityIndicator);
            salesChartStackLayout.Children.Add(chart);
            #endregion

            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            LeadListHeaderView leadListHeaderView = new LeadListHeaderView(async () => await PushTabbedPage());
            leadListHeaderView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(IsVisibleProperty, "IsModelLoaded");
            #endregion

            #region leads list activity inidicator
            ActivityIndicator leadListActivityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.MediumRowHeight
            };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region leadsListView
            LeadListView leadListView = new LeadListView()
            {
                ItemTemplate = new DataTemplate(typeof(LeadListItemCell))
            };
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Content.IsVisible = false;

            await App.Authenticate(PlatformParameters);

            Content.IsVisible = true;

            await ViewModel.ExecuteLoadSeedDataCommand();

            ViewModel.IsInitialized = true;

            Insights.Track("Dashboard Page");
        }

        async Task PushTabbedPage(Account lead = null)
        {
            LeadDetailViewModel viewModel = new LeadDetailViewModel(Navigation, lead); 

            TabbedPage tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new LeadDetailPage(viewModel)
                {
                    Title = TextResources.Details,
                    Icon = new FileImageSource() { File = "LeadDetailTab" }
                });

            tabbedPage.Children.Add(new LeadContactDetailPage(viewModel)
                {
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" }
                });

            await ViewModel.PushModalAsync(tabbedPage);
        }
    }
}

