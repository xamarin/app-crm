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
using Xamarin;
using XamarinCRM.Layouts;
using System.Threading.Tasks;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Statics;
using XamarinCRM.Views.Customers;
using XamarinCRM.Converters;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Customers
{
    public class CustomersPage : ModelBoundContentPage<CustomersViewModel>
    {
        public CustomersPage()
        {
            #region customer list activity inidicator
            ActivityIndicator customerListActivityIndicator = new ActivityIndicator()
            { 
                HeightRequest = Sizes.MediumRowHeight
            };
            customerListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            customerListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            customerListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region customer list
            CustomerListView customerListView = new CustomerListView();
            customerListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "Accounts");
            customerListView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            customerListView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            customerListView.ItemTapped += async (sender, e) =>
            {
                Account account = (Account)e.Item;
                await PushTabbedPage(account);
            };
            #endregion

            #region compose view hierarchy
            Content = new UnspacedStackLayout()
            {
                Children =
                {
                    customerListActivityIndicator,
                    customerListView
                }
            };
            #endregion

            #region wire up MessagingCenter
            // Catch the login success message from the MessagingCenter.
            // This is really only here for Android, which doesn't fire the OnAppearing() method in the same way that iOS does (every time the page appears on screen).
            Device.OnPlatform(Android: () => MessagingCenter.Subscribe<CustomersPage>(this, MessagingServiceConstants.AUTHENTICATED, sender => OnAppearing()));
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
            {
                return;
            }
            ViewModel.LoadAccountsCommand.Execute(null);
            ViewModel.IsInitialized = true;

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERS);
        }

        async Task PushTabbedPage(Account account = null)
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                var customerDetailPage = new CustomerDetailPage(account)
                    {
                        BindingContext = new CustomerDetailViewModel(account) { Navigation = this.Navigation },
                        Title = TextResources.Customers_Detail_Tab_Title,
                        Icon = new FileImageSource() { File = "CustomersTab" } // only used  on iOS
                    };
                await Navigation.PushAsync(customerDetailPage);
            }
            else
            {
                await Navigation.PushAsync(new CustomerTabbedPage(ViewModel.Navigation, account));
            }
        }
    }
}

