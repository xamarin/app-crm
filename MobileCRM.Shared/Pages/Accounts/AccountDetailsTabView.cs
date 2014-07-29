using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
    public class AccountDetailsTabView : TabbedPage
    {
      private AccountDetailsViewModel viewModelAcct;
      private OrdersViewModel viewModelOrder;
      private OrdersViewModel viewModelHistory;

      AccountDetailsView2 viewAcctDetails;
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

              //viewAcctDetails = new AccountDetailsView2(viewModelAcct, viewModelHistory);
              //this.Children.Add(viewAcctDetails);


              //viewAcctOrders = new AccountOrdersView(account.Id) { Title = "Orders" };
              viewAcctOrders = new AccountOrdersView(account.Id, viewModelOrder) { Title = "Orders" };
              this.Children.Add(viewAcctOrders);


              //viewAcctHistory = new AccountHistoryView(account.Id) { Title = "History" };
              viewAcctHistory = new AccountHistoryView(viewModelHistory) { Title = "History" };
              this.Children.Add(viewAcctHistory);

              viewAcctMap = new AccountMapView(viewModelAcct);
              this.Children.Add(viewAcctMap);

          }
          catch (Exception exc)
          {
              Console.WriteLine("EXCEPTION: AccountDetailsTabView.Constructor(): " + exc.Message + "  |  " + exc.StackTrace);
          }

      }  //end ctor



      //protected override void OnAppearing()
      protected async override void OnAppearing()
      {
          base.OnAppearing();


          //viewAcctOrders.RefreshView();
          //viewAcctHistory.RefreshView();


          await viewModelOrder.ExecuteLoadOrdersCommand();
          await viewModelHistory.ExecuteLoadOrdersCommand();

          viewModelAcct.IsInitialized = true;
          viewModelHistory.IsInitialized = true;
          viewModelOrder.IsInitialized = true;

          viewAcctDetails.RefreshView();

      }

    } //end class
}
