using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.Layouts;
using MobileCRM.Pages.Base;
using MobileCRM.Customers;
using Xamarin;
using MobileCRM.Views.Customers;

namespace MobileCRM.Pages.Customers
{
    public class CustomerOrdersPage : ModelEnforcedContentPage<OrdersViewModel>
    {
        const double paddingAmount = 20;

        public CustomerOrdersPage()
        {
            #region activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.LargeRowHeight
            };
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region new order label
            OrderListHeaderView headerView = new OrderListHeaderView();
            headerView.SetBinding(ContentView.IsVisibleProperty, "IsModelLoaded");
            headerView.SetBinding(ContentView.IsEnabledProperty, "IsModelLoaded");
            #endregion

            #region order list view
            CustomerOrderListView customerOrderListView = new CustomerOrderListView();
            customerOrderListView.SetBinding(CustomerOrderListView.ItemsSourceProperty, "Orders");
            customerOrderListView.SetBinding(CustomerOrderListView.IsVisibleProperty, "IsModelLoaded");
            customerOrderListView.SetBinding(CustomerOrderListView.IsEnabledProperty, "IsModelLoaded");
            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(activityIndicator);

            stackLayout.Children.Add(headerView);

            stackLayout.Children.Add(customerOrderListView);

            Content = stackLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.ExecuteLoadOrdersCommand();

            ViewModel.IsInitialized = true;

            Insights.Track("Customer Orders Page");
        }
    }
}

