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
using XamarinCRM.Statics;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Sales;
using XamarinCRM.Converters;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class LeadsView : ModelBoundContentView<SalesDashboardLeadsViewModel>
    {
        public LeadsView()
        {
            #region leads list activity inidicator
            var leadListActivityIndicator = new ActivityIndicator()
            { 
                HeightRequest = RowSizes.MediumRowHeightDouble
            };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region loading label
            var loadingLabel = new Label()
            {
                Text = TextResources.SalesDashboard_Leads_LoadingLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = RowSizes.MediumRowHeightDouble,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            var leadListHeaderView = new LeadListHeaderView(new Command(ExecutePushLeadDetailsTabbedPageCommand));
            leadListHeaderView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            leadListHeaderView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());
            #endregion

            #region leadsListView
            var leadListView = new LeadListView();
            leadListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "Leads");
            leadListView.SetBinding(IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            leadListView.SetBinding(IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            leadListView.ItemTapped += (sender, e) =>
            {
                Account leadListItem = (Account)e.Item;
                ExecutePushLeadDetailsTabbedPageCommand(leadListItem);
            };
            #endregion

            #region compose view hierarchy
            Content = new StackLayout()
            {
                Spacing = 0,
                Children =
                {
                    leadListHeaderView,
                    loadingLabel,
                    leadListActivityIndicator,
                    leadListView
                }
            };
            #endregion
        }

        /// <summary>
        /// We encapsulate ViewModel.PushLeadDetailsTabbedPageCommand.Execute(account) because ViewModel is null during construction of this class.
        /// </summary>
        /// <param name="account">An object of type <see cref="XamarinCRM.Models.Account"/>. Null by default. If null, pushes a fresh lead details tabbed page. If not null, loads the account in the pushed lead details tabbed page.</param>
        void ExecutePushLeadDetailsTabbedPageCommand(object account = null)
        {
            ViewModel.PushTabbedLeadPageCommand.Execute(account);
        }
    }
}

