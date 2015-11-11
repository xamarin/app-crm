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

