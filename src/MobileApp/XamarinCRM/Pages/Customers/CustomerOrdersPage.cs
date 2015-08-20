using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;
using XamarinCRM.Converters;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Custom;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerOrdersPage : ModelBoundContentPage<OrdersViewModel>
    {
        public CustomerOrdersPage(OrdersViewModel viewModel)
        {
            BindingContext = viewModel;

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
            headerView.SetBinding(VisualElement.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            headerView.SetBinding(VisualElement.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region header
            StackLayout companyInfoStackLayout = new UnspacedStackLayout();

            Label companyTitleLabel = new Label()
                {
                    Text = TextResources.Customers_Orders_EditOrder_CompanyTitle,
                    TextColor = Palette._007,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    XAlign = TextAlignment.Start,
                    YAlign = TextAlignment.End,
                    LineBreakMode = LineBreakMode.TailTruncation
                };

            Label companyNameLabel = new Label()
                {
                    TextColor = Palette._006,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    XAlign = TextAlignment.Start,
                    YAlign = TextAlignment.Start,
                    LineBreakMode = LineBreakMode.TailTruncation
                };
            companyNameLabel.SetBinding(Label.TextProperty, "Account.Company");

            RelativeLayout headerLabelsRelativeLayout = new RelativeLayout() { HeightRequest = Sizes.LargeRowHeight };

            headerLabelsRelativeLayout.Children.Add(
                view: companyTitleLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            headerLabelsRelativeLayout.Children.Add(
                view: companyNameLabel,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            ContentView headerLabelsView = new ContentView() { Padding = new Thickness(20, 0), Content = headerLabelsRelativeLayout };

            companyInfoStackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = headerLabelsView });

            companyInfoStackLayout.SetBinding(VisualElement.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            companyInfoStackLayout.SetBinding(VisualElement.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());

            #endregion

            #region order list view
            CustomerOrderListView customerOrderListView = new CustomerOrderListView();
            customerOrderListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "Orders");
            customerOrderListView.SetBinding(VisualElement.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            customerOrderListView.SetBinding(VisualElement.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());

            customerOrderListView.ItemTapped += async (sender, e) =>
            {
                var order = (Order)e.Item;
                await Navigation.PushAsync(new EditOrderPage() { BindingContext = new OrderDetailViewModel(ViewModel.Account, order) { Navigation = Navigation }, });
            };

            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();
            stackLayout.Children.Add(activityIndicator);
            stackLayout.Children.Add(companyInfoStackLayout);
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

