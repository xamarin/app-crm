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

namespace XamarinCRM.Pages.Customers
{
    public class CustomerDetailPage : ModelBoundContentPage<CustomerDetailViewModel>
    {
        public CustomerDetailPage()
        {

            StackLayout stackLayout = new UnspacedStackLayout()
            {
                Children =
                {
                    new CustomerDetailHeaderView(),
                    new CustomerDetailContactView(),
                    new CustomerDetailPhoneView(this),
                    new CustomerDetailAddressView()
                }
            };

            Content = new ScrollView() { Content = stackLayout };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERDETAIL);
        }
    }
}

