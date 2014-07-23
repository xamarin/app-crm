using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
    public class AccountDetailsTabView : TabbedPage
    {
      private AccountDetailsViewModel viewModel;


      //AccountDetailsView viewAcctDetails;
      AccountDetailsView2 viewAcctDetails;

      AccountOrdersView viewAcctOrders;
      AccountHistoryView viewAcctHistory;
      //AccountNotesView2 viewAcctNotes;
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

              viewModel = new AccountDetailsViewModel(account) { Navigation = Navigation };

              //viewAcctDetails = new AccountDetailsView(viewModel);
              viewAcctDetails = new AccountDetailsView2(viewModel);
              this.Children.Add(viewAcctDetails);


              viewAcctOrders = new AccountOrdersView(account.Id) { Title = "Orders" };
              this.Children.Add(viewAcctOrders);


              viewAcctHistory = new AccountHistoryView(account.Id) { Title = "History" };
              this.Children.Add(viewAcctHistory);

              viewAcctMap = new AccountMapView(viewModel);
              this.Children.Add(viewAcctMap);

          }
          catch (Exception exc)
          {
              Console.WriteLine("EXCEPTION: AccountDetailsTabView.Constructor(): " + exc.Message + "  |  " + exc.StackTrace);
          }

      }  //end ctor



      protected override void OnAppearing()
      {
          base.OnAppearing();

          viewAcctOrders.RefreshView(viewModel.IsInitialized);
          viewAcctHistory.RefreshView(viewModel.IsInitialized);

          //TODO:  (SteveYi) temporarily commented out.  Uncomment once graphing in AccountDetailsView is fixed.
          //if (viewModel.IsInitialized)
          //{
          //    return;
          //}

          //viewModel.IsInitialized = true;
      }

    } //end class
}
