﻿//
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
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerTabbedPage : TabbedPage
    {
        public CustomerTabbedPage(INavigation navigation, Account account)
        {
            Title = account.Company;
            // since we're modally presented this tabbed view (because Android doesn't natively support nested tabs),
            // this tool bar item provides a way to get back to the Customers list

            var customerDetailPage = new CustomerDetailPage()
            {
                BindingContext = new CustomerDetailViewModel(account) { Navigation = this.Navigation },
                Title = TextResources.Customers_Detail_Tab_Title,
                Icon = new FileImageSource() { File = "CustomersTab" } // only used  on iOS
            };

            var customerOrdersPage = new CustomerOrdersPage()
            {
                BindingContext = new OrdersViewModel(account) { Navigation = this.Navigation },
                Title = TextResources.Customers_Orders_Tab_Title,
                Icon = new FileImageSource() { File = "ProductsTab" } // only used  on iOS
            };

            var customerSalesPage = new CustomerSalesPage()
            {
                BindingContext = new CustomerSalesViewModel(account) { Navigation = this.Navigation },
                Title = TextResources.Customers_Sales_Tab_Title,
                Icon = new FileImageSource() { File = "SalesTab" } // only used  on iOS
            };

            Children.Add(customerDetailPage);
            Children.Add(customerOrdersPage);
            Children.Add(customerSalesPage);
        }
    }
}

