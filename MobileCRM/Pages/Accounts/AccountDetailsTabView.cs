using MobileCRM;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.ViewModels.Orders;
using System;
using MobileCRM.Models;
using Xamarin.Forms;
using Xamarin;
using MobileCRM.Pages.Orders;

namespace MobileCRM.Pages.Accounts
{
    public class AccountDetailsTabView : TabbedPage
    {
        AccountDetailsViewModel viewModelAcct;
        OrdersViewModel viewModelOrder;
        OrdersViewModel viewModelHistory;

        AccountDetailsView viewAcctDetails;
        AccountOrdersView viewAcctOrders;
        AccountHistoryView viewAcctHistory;
        AccountMapView viewAcctMap;

        public AccountDetailsTabView(Account account)
        {
            try
            {
                if (account != null)
                {
                    this.Title = account.Company;
                }
                else
                {
                    this.Title = "New Lead";
                }

                viewModelAcct = new AccountDetailsViewModel(account) { Navigation = Navigation };
                viewModelOrder = new OrdersViewModel(true, account.Id) { Navigation = Navigation };
                viewModelHistory = new OrdersViewModel(false, account.Id) { Navigation = Navigation };

                viewAcctDetails = new AccountDetailsView(viewModelAcct, viewModelHistory);
                this.Children.Add(viewAcctDetails);

                viewAcctOrders = new AccountOrdersView(account.Id, viewModelOrder) { Title = "Orders" };
                this.Children.Add(viewAcctOrders);

                viewAcctHistory = new AccountHistoryView(viewModelHistory) { Title = "History" };
                this.Children.Add(viewAcctHistory);

                viewAcctMap = new AccountMapView(viewModelAcct);
                this.Children.Add(viewAcctMap);
            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                System.Diagnostics.Debug.WriteLine("EXCEPTION: AccountDetailsTabView.Constructor(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        }
        //end ctor

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModelOrder.IsInitialized)
            {
                await viewModelOrder.ExecuteLoadOrdersCommand();
                await viewModelHistory.ExecuteLoadOrdersCommand();
            }

            viewModelAcct.IsInitialized = true;
            viewModelHistory.IsInitialized = true;
            viewModelOrder.IsInitialized = true;

            viewAcctDetails.RefreshView();
        }
    }
    //end class
}