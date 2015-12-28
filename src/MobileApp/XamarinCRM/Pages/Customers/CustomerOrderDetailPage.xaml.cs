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
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Pages.Products;
using XamarinCRM.ViewModels.Products;
using Xamarin;
using XamarinCRM.Statics;

namespace XamarinCRM.Pages.Customers
{
    public partial class CustomerOrderDetailPage : CustomerOrderDetailPageXaml
    {
        bool _ProductEntry_Focused_Subscribed;

        public CustomerOrderDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetToolBarItems();

            if (!_ProductEntry_Focused_Subscribed)
            {
                productSelectionEntry.Focused += ProductEntry_Focused;
                _ProductEntry_Focused_Subscribed = true;
            } 

            await ViewModel.ExecuteLoadOrderItemImageUrlCommand();

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERORDERDETAIL);
        }

        void SetToolBarItems()
        {
            ToolbarItems.Clear();

            if (ViewModel.Order.IsOpen)
            {
                ToolbarItems.Add(GetSaveToolBarItem());
            }
        }

        ToolbarItem GetSaveToolBarItem()
        {
            ToolbarItem saveToolBarItem = new ToolbarItem();
            saveToolBarItem.Text = TextResources.Save;
            saveToolBarItem.Icon = "save.png";
            saveToolBarItem.Clicked += SaveToolBarItem_Clicked;
            return saveToolBarItem;
        }

        void SaveToolBarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(productSelectionEntry.Text))
            {
                OrderItemNotSelectedAction.Invoke();
            }
            else
            {
                SaveAction.Invoke();
            }
        }

        void DeliverButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(productSelectionEntry.Text))
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

            NavigationPage navPage = new NavigationPage(new CategoryListPage()
                { 
                    BindingContext = new CategoriesViewModel(null, true) { Navigation = this.Navigation },
                    Title = TextResources.MainTabs_Products
                });

            if (Device.OS == TargetPlatform.Android)
                navPage.ToolbarItems.Add(new ToolbarItem(null, "exit.png", () => Navigation.PopModalAsync()));

            if (Device.OS == TargetPlatform.iOS)
                navPage.ToolbarItems.Add(new ToolbarItem(TextResources.Cancel, null, () => Navigation.PopModalAsync()));

            await ViewModel.PushModalAsync(navPage);
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
                            var now = DateTime.UtcNow;
                            ViewModel.Order.IsOpen = false; // close the order
                            ViewModel.Order.ClosedDate = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), DateTimeKind.Utc); // set the closed date
                            ViewModel.SaveOrderCommand.Execute(null);
                        }
                    });
            }
        }
    }

    public abstract class CustomerOrderDetailPageXaml : ModelBoundContentPage<OrderDetailViewModel>
    {

    }
}

