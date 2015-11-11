// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Custom;
using XamarinCRM.Views.Customers;
using XamarinCRM.Models;
using XamarinCRM.Cells.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerOrdersPage : ModelBoundContentPage<OrdersViewModel>
    {
        public CustomerOrdersPage()
        {

            #region toolbar items
            if(Device.OS != TargetPlatform.Android)
            {
                ToolbarItems.Add(new ToolbarItem
                    {
                        Text = "Add",
                        Icon = "add.png",
                        Command = new Command(AddNewOrderTapped)
                    });
            }
            #endregion
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

            addNewOrderImage.IsVisible = Device.OS != TargetPlatform.Android;

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
            var customerOrderListView = new CustomerOrderListView() { IsGroupingEnabled = true };
            customerOrderListView.GroupDisplayBinding = new Binding("Key");
            customerOrderListView.GroupHeaderTemplate = new DataTemplate(typeof(CustomerOrderListViewGroupHeaderCell));
            customerOrderListView.SetBinding(ListView.ItemsSourceProperty, "OrderGroups");
            customerOrderListView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            customerOrderListView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());

            customerOrderListView.ItemTapped += async (sender, e) =>
            {
                var order = (Order)e.Item;
				await Navigation.PushAsync (new CustomerOrderDetailPage()
                    {
                        BindingContext = new OrderDetailViewModel(ViewModel.Account, order)
                        {
                            Navigation = Navigation
                        },
                    });
            };
            #endregion

            #region compose view hierarchy
            var stack = new UnspacedStackLayout()
            { 
                Children =
                { 
                    activityIndicator, 
                    customerOrderListView 
                }
            };

            if (Device.OS == TargetPlatform.Android)
            {
                var fab = new FloatingActionButtonView
                    {
                        ImageName = "fab_add.png",
                        ColorNormal = Palette._001,
                        ColorPressed = Palette._002,
                        ColorRipple = Palette._001,
                        Clicked = (sender, args) => 
                            AddNewOrderTapped(),
                    };

                var absolute = new AbsoluteLayout
                    { 
                        VerticalOptions = LayoutOptions.FillAndExpand, 
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };

                // Position the pageLayout to fill the entire screen.
                // Manage positioning of child elements on the page by editing the pageLayout.
                AbsoluteLayout.SetLayoutFlags(stack, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(stack, new Rectangle(0f, 0f, 1f, 1f));
                absolute.Children.Add(stack);

                // Overlay the FAB in the bottom-right corner
                AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                absolute.Children.Add(fab);

                Content = absolute;

            }
            else
            {
                Content = stack;
            }
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadOrdersCommand.Execute(null);

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERORDERS);
        }

        async void AddNewOrderTapped()
		{
			NavigationPage.SetBackButtonTitle(this, "Back");

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

