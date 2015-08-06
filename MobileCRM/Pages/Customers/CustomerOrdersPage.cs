using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.Layouts;
using MobileCRM.Customers;
using Xamarin;
using MobileCRM.Views.Customers;

namespace MobileCRM.Pages.Customers
{
    public class CustomerOrdersPage : BaseCustomerDetailPage<OrdersViewModel>
    {
        public CustomerOrdersPage()
        {
            #region activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator() { HeightRequest = Sizes.LargeRowHeight };
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region new order label
            OrderListHeaderView headerView = new OrderListHeaderView();
            TapGestureRecognizer newOrderTapGestureRecognizer = new TapGestureRecognizer()
            { 
                Command = new Command(async () =>
                    await ViewModel.PushModalAsync(new EditOrderPage()
                        {
                            BindingContext = new OrderDetailViewModel() { Navigation = ViewModel.Navigation }
                        })), 
                NumberOfTapsRequired = 1 
            };
            headerView.AddNewOrderImage.GestureRecognizers.Add(newOrderTapGestureRecognizer);
            headerView.AddNewOrderTextLabel.GestureRecognizers.Add(newOrderTapGestureRecognizer);
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

            StackLayout.Children.Add(stackLayout);

            Content = StackLayout;
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

