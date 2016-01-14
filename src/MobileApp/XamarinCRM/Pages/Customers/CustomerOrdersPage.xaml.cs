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
using Xamarin.Forms;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Models;
using Xamarin;
using XamarinCRM.Statics;

namespace XamarinCRM.Pages.Customers
{
    public partial class CustomerOrdersPage : CustomerOrdersPageXaml
    {
        public CustomerOrdersPage()
        {
            InitializeComponent();

            AddNewOrderButton.Clicked = AddNewOrderButtonClicked;

            if (Device.OS == TargetPlatform.iOS)
            {
                SetToolBarItems();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadOrdersCommand.Execute(null);

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERORDERS);
        }

        async void OrderItemTapped(object sender, ItemTappedEventArgs e)
        {
            var order = (Order)e.Item;
            await Navigation.PushAsync(new CustomerOrderDetailPage()
                {
                    BindingContext = new OrderDetailViewModel(ViewModel.Account, order)
                    {
                        Navigation = Navigation
                    },
                });
        }

        Action<object, EventArgs> AddNewOrderButtonClicked
        {
            get
            {
                return new Action<object, EventArgs>(async (o, e) =>
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
                    });
            }
        }

        void SetToolBarItems()
        {
            ToolbarItems.Clear();

            ToolbarItems.Add(GetNewOrderToolBarItem());
        }

        ToolbarItem GetNewOrderToolBarItem()
        {
            ToolbarItem newOrderButton = new ToolbarItem();
            newOrderButton.Text = "Add";
            newOrderButton.Icon = "add_ios_gray";
            newOrderButton.Clicked += (o, ea) => AddNewOrderButtonClicked.Invoke(o, ea);
            return newOrderButton;
        }
    }

    /// <summary>
    /// This class definition just gives us a way to reference ModelBoundContentPage<T> in the XAML of this Page.
    /// </summary>
    public abstract class CustomerOrdersPageXaml : ModelBoundContentPage<OrdersViewModel>
    {
        
    }
}

