using System;
using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Base;
using XamarinCRM.Pages.Products;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Custom;
using XamarinCRM.ViewModels.Products;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerOrderDetailPage : ModelBoundContentPage<OrderDetailViewModel>
    {
        readonly Thickness _FieldLabelPadding = new Thickness(0, 0, 5, 0);

        const double RowHeight = 30;

        Entry _ProductSelectionEntry;

        bool _ProductEntry_Focused_Subscribed;

        Image _OrderItemImage;

        public CustomerOrderDetailPage()
        {
            // Hide the back button, because we have ToolBarItems to control navigtion on this page.
            // A back button would be confusing here in this modally presented tab page.
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

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

            #endregion

            #region grid setup
            Grid orderDetailsGrid = new Grid()
            {
                Padding = new Thickness(20),
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition { Height = GridLength.Auto },
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
            #endregion

            #region product selection
            _ProductSelectionEntry = new Entry() { Placeholder = TextResources.Customers_Orders_EditOrder_ProductEntryPlaceholder };
            _ProductSelectionEntry.SetBinding(Entry.TextProperty, "Order.Item", BindingMode.TwoWay);
            _ProductSelectionEntry.SetBinding(IsEnabledProperty, "Order.IsOpen");
            _ProductSelectionEntry.SetBinding(IsVisibleProperty, "Order.IsOpen");

            Label productSelectionLabel = new Label() { HeightRequest = RowHeight, YAlign = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            productSelectionLabel.SetBinding(Label.TextProperty, "Order.Item");
            productSelectionLabel.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            productSelectionLabel.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());

            StackLayout productSelectionStack = new UnspacedStackLayout()
            {
                Children =
                {
                    _ProductSelectionEntry,
                    productSelectionLabel
                }
            };
            #endregion

            #region price
            Entry priceEntry = new Entry() { Placeholder = TextResources.Customers_Orders_EditOrder_PriceEntryPlaceholder, Keyboard = Keyboard.Numeric };
            priceEntry.SetBinding(Entry.TextProperty, "Order.Price", BindingMode.TwoWay, new CurrencyDoubleConverter());
            priceEntry.SetBinding(IsEnabledProperty, "Order.IsOpen");
            priceEntry.SetBinding(IsVisibleProperty, "Order.IsOpen");

            Label priceLabel = new Label() { HeightRequest = RowHeight, YAlign = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            priceLabel.SetBinding(Label.TextProperty, "Order.Price", converter: new CurrencyDoubleConverter());
            priceLabel.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            priceLabel.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());

            StackLayout priceStack = new UnspacedStackLayout()
            {
                Children =
                {
                    priceEntry,
                    priceLabel
                }
            };
            #endregion

            #region order date
            Label orderDateLabel = new Label() { HeightRequest = RowHeight, YAlign = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            orderDateLabel.SetBinding(Label.TextProperty, "Order.OrderDate", converter: new ShortDatePatternConverter());
            #endregion

            #region due date
            DatePicker dueDateEntry = new DatePicker();
            dueDateEntry.SetBinding(DatePicker.DateProperty, "Order.DueDate", BindingMode.TwoWay);
            dueDateEntry.SetBinding(IsEnabledProperty, "Order.IsOpen");
            dueDateEntry.SetBinding(IsVisibleProperty, "Order.IsOpen");

            Label dueDateLabel = new Label() { HeightRequest = RowHeight, YAlign = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            dueDateLabel.SetBinding(Label.TextProperty, "Order.DueDate", converter: new ShortDatePatternConverter());
            dueDateLabel.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            dueDateLabel.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());

            StackLayout dueDateStack = new UnspacedStackLayout();
            dueDateStack.Children.Add(dueDateEntry);
            dueDateStack.Children.Add(dueDateLabel);
            #endregion

            #region closed date
            Label closedDateLabel = new Label() { HeightRequest = RowHeight, YAlign = TextAlignment.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            closedDateLabel.SetBinding(Label.TextProperty, "Order.ClosedDate", converter: new ShortDatePatternConverter());
            closedDateLabel.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            closedDateLabel.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            #endregion

            #region product image
            _OrderItemImage = new Image() { Aspect = Aspect.AspectFit };
            _OrderItemImage.SetBinding(Image.SourceProperty, "OrderItemImageUrl");
            #endregion

            #region loading label
            Label loadingImageUrlLabel = new Label()
            {
                Text = TextResources.Customers_Orders_EditOrder_LoadingImageLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = Sizes.MediumRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingImageUrlLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingImageUrlLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region loading label
            Label loadingImageLabel = new Label()
            {
                Text = TextResources.Customers_Orders_EditOrder_LoadingImageLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = Sizes.MediumRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingImageLabel.SetBinding(IsEnabledProperty, new Binding("IsLoading", source: _OrderItemImage));
            loadingImageLabel.SetBinding(IsVisibleProperty, new Binding("IsLoading", source: _OrderItemImage));
            #endregion

            #region image url fetching activity indicator
            ActivityIndicator imageUrlFetchingActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.LargeRowHeight
            };
            imageUrlFetchingActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            imageUrlFetchingActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            imageUrlFetchingActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region image fetching activity indicator
            ActivityIndicator imageFetchingActivityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.LargeRowHeight
            };
            imageFetchingActivityIndicator.SetBinding(IsEnabledProperty, new Binding("IsLoading", source: _OrderItemImage));
            imageFetchingActivityIndicator.SetBinding(IsVisibleProperty, new Binding("IsLoading", source: _OrderItemImage));
            imageFetchingActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: _OrderItemImage));
            #endregion

            #region deliver action button
            Button deliverButton = new Button() { Text = "Deliver Order" };
            deliverButton.Clicked += DeliverButton_Clicked;
            deliverButton.SetBinding(IsEnabledProperty, "Order.IsOpen");
            deliverButton.SetBinding(IsVisibleProperty, "Order.IsOpen");
            #endregion

            #region compose grid contents
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_ProductTitleLabel), 0, 0);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_PriceTitleLabel), 0, 1);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_OrderDateTitleLabel), 0, 2);
            orderDetailsGrid.Children.Add(GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_DueDateTitleLabel), 0, 3);
            var closedDateFieldLabelView = GetFieldLabelContentView(TextResources.Customers_Orders_EditOrder_ClosedDateTitleLabel);
            closedDateFieldLabelView.SetBinding(IsVisibleProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            closedDateFieldLabelView.SetBinding(IsEnabledProperty, "Order.IsOpen", converter: new InverseBooleanConverter());
            orderDetailsGrid.Children.Add(closedDateFieldLabelView, 0, 4);

            orderDetailsGrid.Children.Add(productSelectionStack, 1, 0);
            orderDetailsGrid.Children.Add(priceStack, 1, 1);
            orderDetailsGrid.Children.Add(orderDateLabel, 1, 2);
            orderDetailsGrid.Children.Add(dueDateStack, 1, 3);
            orderDetailsGrid.Children.Add(closedDateLabel, 1, 4);

            orderDetailsGrid.Children.Add(deliverButton, 0, 5);
            Grid.SetColumnSpan(deliverButton, 2);
            #endregion

            #region compose view hierarchy
            Content = new ScrollView()
            { 
                Content = new UnspacedStackLayout()
                {
                    Children =
                    {
                        new ContentViewWithBottomBorder() { Content = headerLabelsView },
                        new ContentViewWithBottomBorder() { Content = orderDetailsGrid },
                        loadingImageUrlLabel,
                        imageUrlFetchingActivityIndicator,
                        loadingImageLabel,
                        imageFetchingActivityIndicator,
                        new ContentView() { Content = _OrderItemImage, Padding = new Thickness(20) }
                    }
                }
            };
            #endregion
        }

        void DeliverButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_ProductSelectionEntry.Text))
            {
                OrderItemNotSelectedAction.Invoke();
            }
            else
            {
                DeliverAction.Invoke();
            }
        }

        async void ProductEntry_Focused(object sender, FocusEventArgs e)
        {
            // Prevents the keyboard on Android from appearing over the modally presented product category list.
            // This is not normally something you need to worry about, but since we're presenting a new page
            // when the entry field is clicked (and because the OS pops the keyboard by default for that), 
            // we need to deal with it by manually unfocusing the entry field. No native platform code required! :)
            Device.OnPlatform(Android: ((Entry)sender).Unfocus);

            NavigationPage navPage = new NavigationPage(new CategoryListPage(null, true)
                { 
                    BindingContext = new CategoriesViewModel(),
                    Title = TextResources.MainTabs_Products
                });
            
            navPage.ToolbarItems.Add(new ToolbarItem(TextResources.Cancel, null, () => Navigation.PopModalAsync()));

            await ViewModel.PushModalAsync(navPage);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetToolBarItems();

            if (!_ProductEntry_Focused_Subscribed)
            {
                _ProductSelectionEntry.Focused += ProductEntry_Focused;
                _ProductEntry_Focused_Subscribed = true;
            } 

            await ViewModel.ExecuteLoadOrderItemImageUrlCommand();
        }

        void SetToolBarItems()
        {
            ToolbarItems.Clear();

            if (ViewModel.Order.IsOpen)
            {
                ToolbarItems.Add(GetSaveToolBarItem());
            }

            ToolbarItems.Add(GetExitToolbarItem());
        }

        ToolbarItem GetSaveToolBarItem()
        {
            ToolbarItem saveToolBarItem = new ToolbarItem();
            saveToolBarItem.Text = TextResources.Save;
            saveToolBarItem.Clicked += SaveToolBarItem_Clicked;
            return saveToolBarItem;
        }

        ToolbarItem GetExitToolbarItem()
        {
            ToolbarItem exitToolBarItem = new ToolbarItem();
            exitToolBarItem.Text = TextResources.Exit;
            exitToolBarItem.Clicked += ExitToolBarItem_Clicked;
            return exitToolBarItem;
        }

        void SaveToolBarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_ProductSelectionEntry.Text))
            {
                OrderItemNotSelectedAction.Invoke();
            }
            else
            {
                SaveAction.Invoke();
            }
        }

        void ExitToolBarItem_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.Order.IsOpen)
            {
                ExitAndDiscardAction.Invoke();
            }
            else
            {
                ExitAction.Invoke();
            }
        }

        Action ExitAction
        {
            get { return new Action(async () => await Navigation.PopAsync()); }
        }

        Action ExitAndDiscardAction
        {
            get
            {
                return new Action(async () =>
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
                    });
            }
        }

        Action OrderItemNotSelectedAction
        {
            get
            {
                return new Action(async () =>
                    {
                        await DisplayAlert(
                            title: TextResources.Customers_Orders_EditOrder_OrderItemNotSelectedConfirmTitle,
                            message: TextResources.Customers_Orders_EditOrder_OrderItemNotSelectedConfirmDescription, 
                            cancel: TextResources.Customers_Orders_EditOrder_OkayConfirmTitle);
                    });
            }
        }

        Action SaveAction
        {
            get
            {
                return new Action(async () =>
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
                    });
            }
        }

        Action DeliverAction
        {
            get
            {
                return new Action(async () =>
                    {
                        var answer = 
                            await DisplayAlert(
                                title: TextResources.Customers_Orders_EditOrder_DeliverConfirmTitle,
                                message: TextResources.Customers_Orders_EditOrder_DeliverConfirmDescription,
                                accept: TextResources.Customers_Orders_EditOrder_DeliverConfirmAffirmative,
                                cancel: TextResources.Cancel);

                        if (answer)
                        {
                            ViewModel.Order.IsOpen = false; // close the order
                            ViewModel.Order.ClosedDate = DateTime.UtcNow; // set the closed date
                            ViewModel.SaveOrderCommand.Execute(null);

                            await Navigation.PopAsync();
                        }
                    });
            }
        }

        ContentView GetFieldLabelContentView(string labelValue)
        {
            return new ContentView()
            {
                HeightRequest = RowHeight,
                Padding = _FieldLabelPadding,
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

