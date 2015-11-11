//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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


                var infoStack = new UnspacedStackLayout()
                    {
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
                
              

                Content = new UnspacedStackLayout
                    {
                        Children =
                            {
                                new CustomerDetailHeaderView(),
                                table
                            }
                    };
            }
            else
            {

                var stackLayout = new UnspacedStackLayout()
                    {
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

