using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Custom;
using XamarinCRM.Views.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerOrdersPage : ModelBoundContentPage<OrdersViewModel>
    {
        public CustomerOrdersPage()
        {
            #region activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator() { HeightRequest = Sizes.LargeRowHeight };
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region header
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

            Image addNewOrderImage = new Image() { Aspect = Aspect.AspectFit };
            Device.OnPlatform(
                iOS: () => addNewOrderImage.Source = new FileImageSource(){ File = "add_ios_blue" }, 
                Android: () => addNewOrderImage.Source = new FileImageSource() { File = "add_android_blue" }
            );
            addNewOrderImage.GestureRecognizers.Add(new TapGestureRecognizer()
                { 
                    Command = new Command(AddNewOrderTapped),
                    NumberOfTapsRequired = 1 
                });

            AbsoluteLayout headerAbsoluteLayout = new AbsoluteLayout() { HeightRequest = Sizes.LargeRowHeight };

            headerAbsoluteLayout.Children.Add(
                view: new UnspacedStackLayout()
                { 
                    Children =
                    {
                        companyTitleLabel,
                        companyNameLabel
                    } 
                }, 
                bounds: new Rectangle(0, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), 
                flags: AbsoluteLayoutFlags.PositionProportional
            );

            headerAbsoluteLayout.Children.Add(
                view: addNewOrderImage,
                bounds: new Rectangle(1, .5, AbsoluteLayout.AutoSize, Device.OnPlatform(.5, .4, .5)),
                flags: AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.HeightProportional
            );

            ContentView headerLabelsView = new ContentView() { Content = headerAbsoluteLayout, Padding = new Thickness(20, 0) };

            headerLabelsView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            headerLabelsView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region order list view
            CustomerOrderListView customerOrderListView = new CustomerOrderListView() { IsGroupingEnabled = true };
            customerOrderListView.GroupDisplayBinding = new Binding("Key");
            customerOrderListView.GroupHeaderTemplate = new DataTemplate(typeof(CustomerOrderListViewGroupHeaderCell));
            customerOrderListView.SetBinding(ListView.ItemsSourceProperty, "OrderGroups");
            customerOrderListView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            customerOrderListView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());

            customerOrderListView.ItemTapped += async (sender, e) =>
            await App.ExecuteIfConnected(async () =>
                {
                    var order = (Order)e.Item;
                    await Navigation.PushAsync(new CustomerOrderDetailPage()
                        {
                            BindingContext = new OrderDetailViewModel(ViewModel.Account, order)
                            {
                                Navigation = Navigation
                            },
                        });
                });
            #endregion

            #region compose view hierarchy
            Content = new UnspacedStackLayout()
            { 
                Children =
                { 
                    activityIndicator, 
                    new ContentViewWithBottomBorder() { Content = headerLabelsView },
                    customerOrderListView 
                }
            };
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Customer Orders Page");

            ViewModel.LoadOrdersCommand.Execute(null);
        }

        async void AddNewOrderTapped()
        {
            await Navigation.PushAsync(
                new CustomerOrderDetailPage()
                {
                    BindingContext = new OrderDetailViewModel(ViewModel.Account)
                    { 
                        Navigation = ViewModel.Navigation 
                    }
                });
        }
    }
}

