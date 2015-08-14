using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerOrdersPage : ModelTypedContentPage<OrdersViewModel>
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
                        await Navigation.PushAsync(new EditOrderPage()
                        {
                            BindingContext = new OrderDetailViewModel(ViewModel.Account) { Navigation = ViewModel.Navigation }
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

            customerOrderListView.ItemTapped += async (sender, e) =>
            {
                var order = (Order)e.Item;
                await Navigation.PushAsync(new EditOrderPage() { BindingContext = new OrderDetailViewModel(ViewModel.Account, order) { Navigation = Navigation }, });
            };

            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(activityIndicator);

            stackLayout.Children.Add(headerView);

            stackLayout.Children.Add(customerOrderListView);

            Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Customer Orders Page");

            ViewModel.LoadOrdersCommand.Execute(null);
        }
    }
}

