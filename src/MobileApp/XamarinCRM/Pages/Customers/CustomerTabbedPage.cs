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

            var customerDetailPage = new CustomerDetailPage(account)
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

