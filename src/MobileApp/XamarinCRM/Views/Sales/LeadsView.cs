using Xamarin.Forms;
using XamarinCRM.Views.Sales;
using XamarinCRM.Statics;
using XamarinCRM.Models;
using XamarinCRM.Layouts;
using XamarinCRM.Views.Base;
using System.Collections.Generic;
using System;

namespace XamarinCRM
{
    public class LeadsView : ModelTypedContentView<SalesDashboardLeadsViewModel>
    {
        public LeadsView()
        {
            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            LeadListHeaderView leadListHeaderView = new LeadListHeaderView(() => ViewModel.PushLeadDetailsTabbedPageCommand.Execute(null));
            leadListHeaderView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListHeaderView.SetBinding(IsVisibleProperty, "IsModelLoaded");
            #endregion

            #region leads list activity inidicator
            ActivityIndicator leadListActivityIndicator = new ActivityIndicator()
                { 
                    HeightRequest = Sizes.MediumRowHeight
                };
            leadListActivityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            leadListActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region leadsListView
            LeadListView leadListView = new LeadListView();
            leadListView.SetBinding(LeadListView.ItemsSourceProperty, "Leads");
            leadListView.SetBinding(IsEnabledProperty, "IsModelLoaded");
            leadListView.SetBinding(IsVisibleProperty, "IsModelLoaded");

            leadListView.ItemTapped += (sender, e) =>
                {
                    Account leadListItem = (Account)e.Item;
                    ViewModel.PushLeadDetailsTabbedPageCommand.Execute(leadListItem);
                };
            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(leadListHeaderView);
            stackLayout.Children.Add(leadListActivityIndicator);
            stackLayout.Children.Add(leadListView);

            Content = stackLayout;
        }
    }
}

