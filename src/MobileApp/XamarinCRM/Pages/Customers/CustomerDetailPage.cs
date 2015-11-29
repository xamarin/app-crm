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
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Customers;
using XamarinCRM.Pages.Base;
using Xamarin;
using XamarinCRM.Statics;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage(Account account)
        {



            if (Device.OS == TargetPlatform.iOS)
            {
                
                var orders = new ImageCell
                {
                    Text = TextResources.Customers_Orders_Tab_Title,
                    ImageSource = new FileImageSource() { File = "ProductsTab" },
                    StyleId = "disclosure",
                    Command = new Command(async() => await Navigation.PushAsync(new CustomerOrdersPage()
                            {
                                BindingContext = new OrdersViewModel(account) { Navigation = this.Navigation },
                                Title = TextResources.Customers_Orders_Tab_Title,
                                Icon = new FileImageSource() { File = "ProductsTab" } // only used  on iOS
                            }))
                };

                var sales = new ImageCell
                {
                    Text = TextResources.Customers_Sales_Tab_Title,
                    ImageSource = new FileImageSource() { File = "SalesTab" },
                    StyleId = "disclosure",
                    Command = new Command(async () => await Navigation.PushAsync(new CustomerSalesPage()
                            {
                                BindingContext = new CustomerSalesViewModel(account) { Navigation = this.Navigation },
                                Title = TextResources.Customers_Sales_Tab_Title,
                                Icon = new FileImageSource() { File = "SalesTab" } // only used  on iOS
                            }))
                };


                var infoStack = new StackLayout()
                {
                    Spacing = 0,
                    Children =
                    {
                                
                        new CustomerDetailContactView(),
                        new CustomerDetailPhoneView(this),
                        new CustomerDetailAddressView(),
                    }
                };
                
                var table = new TableView()
                {
                    Intent = TableIntent.Menu,
                    HasUnevenRows = true,
                    Root = new TableRoot()
                    {
                        new TableSection("Details")
                        {
                            new ViewCell
                            {
                                View = infoStack,
                                Height = 280
                            }
                        },
                        new TableSection("More")
                        {
                            orders,
                            sales
                        }
                    }
                };
                
              

                Content = new StackLayout
                {
                    Spacing = 0,
                    Children =
                    {
                        new CustomerDetailHeaderView(),
                        table
                    }
                };
            }
            else
            {

                var stackLayout = new StackLayout()
                {
                    Spacing = 0,
                    Children =
                    {
                        new CustomerDetailHeaderView(),
                        new CustomerDetailContactView(),
                        new CustomerDetailPhoneView(this),
                        new CustomerDetailAddressView(),
                    }
                };

                Content = new ScrollView() { Content = stackLayout };
            }



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERDETAIL);
        }
    }
}

