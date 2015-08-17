using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.Views.Base;
using XamarinCRM.Views.Sales;

namespace XamarinCRM
{
    public class LeadsView : ModelTypedContentView<SalesDashboardLeadsViewModel>
    {
        public LeadsView()
        {
            #region leads list header
            // LeadListHeaderView is an example of a custom view composed with Xamarin.Forms.
            // It takes an action as a constructor parameter, which will be used by the add new lead button ("+").
            LeadListHeaderView leadListHeaderView = new LeadListHeaderView(new Command(ExecutePushLeadDetailsTabbedPageCommand));
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
                    ExecutePushLeadDetailsTabbedPageCommand(leadListItem);
                };
            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(leadListHeaderView);
            stackLayout.Children.Add(leadListActivityIndicator);
            stackLayout.Children.Add(leadListView);

            Content = stackLayout;
        }

        /// <summary>
        /// We encapsulate ViewModel.PushLeadDetailsTabbedPageCommand.Execute(account) because ViewModel is null during construction of this class.
        /// </summary>
        /// <param name="account">An Account. Null by default. If null, pushes a fresh lead details tabbed page. If not null, loads the account in the pushed lead details tabbed page.</param>
        void ExecutePushLeadDetailsTabbedPageCommand(object account = null)
        {
            ViewModel.PushLeadDetailsTabbedPageCommand.Execute(account);
        }
    }
}

