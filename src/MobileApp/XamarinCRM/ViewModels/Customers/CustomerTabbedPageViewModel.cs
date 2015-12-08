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
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;
using Xamarin.Forms;

namespace XamarinCRM.ViewModels.Customers
{
    public class CustomerTabbedPageViewModel : BaseViewModel
    {
        public CustomerTabbedPageViewModel(Account account, Page currentPage)
        {
            _Account = account;

            _CustomerDetailViewModel = new CustomerDetailViewModel(_Account, currentPage);

            _OrdersViewModel = new OrdersViewModel(_Account);

            _CustomerSalesViewModel = new CustomerSalesViewModel(_Account);
        }

        Account _Account;
        public Account Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
                OnPropertyChanged("Account");
            }
        }

        CustomerDetailViewModel _CustomerDetailViewModel;
        public CustomerDetailViewModel CustomerDetailViewModel
        {
            get { return _CustomerDetailViewModel; }
            private set 
            {
                _CustomerDetailViewModel = value;
                OnPropertyChanged("CustomerDetailViewModel");
            }
        }

        OrdersViewModel _OrdersViewModel;
        public OrdersViewModel OrdersViewModel
        {
            get { return _OrdersViewModel; }
            private set 
            {
                _OrdersViewModel = value;
                OnPropertyChanged("OrdersViewModel");
            }
        }

        CustomerSalesViewModel _CustomerSalesViewModel;
        public CustomerSalesViewModel CustomerSalesViewModel
        {
            get { return _CustomerSalesViewModel; }
            private set 
            {
                _CustomerSalesViewModel = value;
                OnPropertyChanged("CustomerSalesViewModel");
            }
        }
    }
}

