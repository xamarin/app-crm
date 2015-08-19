using System;
using XamarinCRM.Pages.Base;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Converters;
using XamarinCRM.Pages.Products;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM.Pages.Customers
{
    public class EditOrderPage : ModelTypedContentPage<OrderDetailViewModel>
    {
        Thickness fieldLabelThickness = new Thickness(0, 0, 5, 0);

        const double rowHeight = 30;

        public EditOrderPage()
        {
            SetToolBarItems();

            // Hide the back button, because we have ToolBarItems to control navigtion on this page.
            // A back button would be confusing here in this modally presented tab page.
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            #region header
            StackLayout headerStackLayout = new UnspacedStackLayout();

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

            headerStackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = headerLabelsView });
            #endregion

            #region fields
            Grid orderDetailsGrid = new Grid()
            {
                Padding = new Thickness(20),
                RowSpacing = 15,
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
            };
            
            Entry productEntry = new Entry() { Placeholder = TextResources.Customers_Orders_EditOrder_ProductEntryPlaceholder };
            productEntry.SetBinding(Entry.TextProperty, "Order.Item", BindingMode.TwoWay);
            productEntry.Focused += async (sender, e) =>
            {
                // prevents the keyboard on Android from appearing over the modally presented product category list
                Device.OnPlatform(Android: productEntry.Unfocus);

                NavigationPage navPage = new NavigationPage(new CategoryListPage(null, null, true)
                    { 
                        Title = TextResources.MainTabs_Products
                    });
                navPage.ToolbarItems.Add(new ToolbarItem(TextResources.Cancel, null, () => Navigation.PopModalAsync()));
                await ViewModel.PushModalAsync(navPage);
            };

            Entry priceEntry = new Entry() { Placeholder = TextResources.Customers_Orders_EditOrder_PriceEntryPlaceholder };
            priceEntry.SetBinding(Entry.TextProperty, "Order.Price", BindingMode.TwoWay, new CurrencyDoubleConverter());

            DatePicker orderDateEntry = new DatePicker() { IsEnabled = false };
            orderDateEntry.SetBinding(DatePicker.DateProperty, "Order.OrderDate");

            DatePicker dueDateEntry = new DatePicker();
            dueDateEntry.SetBinding(DatePicker.DateProperty, "Order.DueDate", BindingMode.TwoWay);
            dueDateEntry.SetBinding(IsEnabledProperty, "Order.IsOpen");

            DatePicker closedDateEntry = new DatePicker() { IsEnabled = false };
            closedDateEntry.SetBinding(DatePicker.DateProperty, "Order.ClosedDate");
            #endregion

            #region compose view hierarchy
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_ProductTitleLabel), 0, 0);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_PriceTitleLabel), 0, 1);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_OrderDateTitleLabel), 0, 2);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_DueDateTitleLabel), 0, 3);
            var closedDateFieldLabelView = GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_ClosedDateTitleLabel);
            closedDateFieldLabelView.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            closedDateFieldLabelView.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            orderDetailsGrid.Children.Add(closedDateFieldLabelView, 0, 4);

            orderDetailsGrid.Children.Add(productEntry, 1, 0);
            orderDetailsGrid.Children.Add(priceEntry, 1, 1);
            orderDetailsGrid.Children.Add(orderDateEntry, 1, 2);
            orderDetailsGrid.Children.Add(dueDateEntry, 1, 3);
            closedDateEntry.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            closedDateEntry.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            orderDetailsGrid.Children.Add(closedDateEntry, 1, 4);

            StackLayout stackLayout = new UnspacedStackLayout();
            stackLayout.Children.Add(headerStackLayout);
            stackLayout.Children.Add(new ContentViewWithBottomBorder() { Content = orderDetailsGrid });
            #endregion

            Content = stackLayout;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }

        void SetToolBarItems()
        {
            ToolbarItems.Add(
                new ToolbarItem(TextResources.Save, null, async () =>
                    {
                        var answer = 
                            await DisplayAlert(
                                title: TextResources.Customers_Orders_EditOrder_SaveConfirmTitle,
                                message: TextResources.Customers_Orders_EditOrder_SaveConfirmDescription,
                                accept: TextResources.Save,
                                cancel: TextResources.Cancel);

                        if (answer)
                        {
                            ViewModel.SaveOrderCommand.Execute(null);

                            await Navigation.PopAsync();
                        }
                    }));

            ToolbarItems.Add(
                new ToolbarItem(TextResources.Exit, null, async () =>
                    {
                        {
                            var answer = 
                                await DisplayAlert(
                                    title: TextResources.Customers_Orders_EditOrder_ExitConfirmTitle,
                                    message: TextResources.Customers_Orders_EditOrder_ExitConfirmDescription,
                                    accept: TextResources.Exit_and_Discard,
                                    cancel: TextResources.Cancel);

                            if (answer)
                            {
                                await Navigation.PopAsync();
                            }
                        }
                    }));
        }

        ContentView GetFieldLabelContentView(string labelValue)
        {
            return new ContentView()
            {
                HeightRequest = rowHeight,
                Padding = fieldLabelThickness,
                Content = new Label()
                {
                    Text = labelValue.CapitalizeForAndroid(), 
                    XAlign = TextAlignment.End, 
                    YAlign = TextAlignment.Center,
                    TextColor = Device.OnPlatform(Palette._007, Palette._004, Palette._007),
                }
            };
        }
    }
}

