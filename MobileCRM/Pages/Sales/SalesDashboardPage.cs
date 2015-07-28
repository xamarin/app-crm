using System.Threading.Tasks;
using MobileCRM.Cells;
using MobileCRM.Localization;
using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.ViewModels.Sales;
using MobileCRM.Views.Sales;
using Xamarin;
using Xamarin.Forms;

namespace MobileCRM.Pages.Sales
{
    public class SalesDashboardPage : BaseContentPage
    {
        SalesDashboardViewModel ViewModel
        {
            get { return BindingContext as SalesDashboardViewModel; }
        }

        public SalesDashboardPage(SalesDashboardViewModel viewModel)
        {
            SetBinding(TitleProperty, new Binding() { Source = TextResources.Sales });

            BindingContext = viewModel;

            #region sales graph header
            SalesChartHeaderView chartHeaderView = new SalesChartHeaderView() { BindingContext = ViewModel };
            chartHeaderView.WeeklyAverageValueLabel.SetBinding(Label.TextProperty, "SalesAverage");
            #endregion

            #region the sales graph
            SalesChartView chartView = new SalesChartView(ViewModel);

            #endregion

            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            LeadListHeaderView leadListHeaderView = new LeadListHeaderView(async () => await PushTabbedLeadPage());
            leadListHeaderView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(ActivityIndicator.IsRunningProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(IsVisibleProperty, "IsModelLoaded");
            #endregion

            #region leads list activity inidicator
            ActivityIndicator leadListActivityIndicator = new ActivityIndicator()
            { 
                BindingContext = ViewModel,
                HeightRequest = Sizes.MediumRowHeight
            };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region leadsListView
            LeadListView leadListView = new LeadListView()
            {
                BindingContext = ViewModel,
                ItemTemplate = new DataTemplate(typeof(LeadListItemCell))
            };
            leadListView.SetBinding(LeadListView.ItemsSourceProperty, "Leads");
            leadListView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListView.SetBinding(IsVisibleProperty, "IsModelLoaded");

            leadListView.ItemTapped += async (sender, e) =>
            {
                Account leadListItem = (Account)e.Item;
                await PushTabbedLeadPage(leadListItem);
            };
            #endregion

            #region setup stack layout and add children
            // Instantiate a StackLayout that several view elements will be added to.
            StackLayout stackLayout = new StackLayout() { Spacing = 0 };

            // conditionally set the top padding to 20px to account for the iOS status bar
            Device.OnPlatform(iOS: () => stackLayout.Padding = new Thickness(0, 20, 0, 0));

            stackLayout.Children.Add(chartHeaderView);

            stackLayout.Children.Add(chartView);

            stackLayout.Children.Add(leadListHeaderView);

            stackLayout.Children.Add(leadListActivityIndicator);

            stackLayout.Children.Add(leadListView);
            #endregion

            // assign the built-up stack layout to the Content property of this page
            Content = new ScrollView() { Content = stackLayout };

            Content.IsVisible = false;
        }

        protected override async void ExecuteOnlyIfAuthenticated()
        {
            await ViewModel.ExecuteLoadSeedDataCommand();

            ViewModel.IsInitialized = true;

            Insights.Track("Dashboard Page");
        }

        /// <summary>
        /// Performs a modal push of a LeadDetailTabbedPage.
        /// </summary>
        /// <param name="model">A <see cref="XamarinCRM.LeadListItemViewModel"/>.</param>
        async Task PushTabbedLeadPage(Account lead = null)
        {
            await ViewModel.Navigation.PushModalAsync(new LeadDetailTabbedPage(new LeadDetailViewModel(Navigation, lead)));
        }
    }
}

