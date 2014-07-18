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


      AccountDetailsView viewAcctDetails;
      AccountOrdersView viewAcctOrders;
      AccountHistoryView viewAcctHistory;
      AccountNotesView2 viewAcctNotes;


      public AccountDetailsTabView(Account account)
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

        viewAcctDetails = new AccountDetailsView(viewModel);
        this.Children.Add(viewAcctDetails);
        //this.Children.Add(new AccountDetailsView(viewModel));
        //this.Children.Add(new AccountDetailsView2(viewModel));

        viewAcctOrders = new AccountOrdersView(account.Id) { Title = "Orders" };
        this.Children.Add(viewAcctOrders);


        viewAcctHistory = new AccountHistoryView(account.Id) { Title = "History" };
        this.Children.Add(viewAcctHistory);


        //this.Children.Add(new AccountNotesView(account)
        //{
        //  Title = "Notes"
        //});
        this.Children.Add(new AccountNotesView2(viewModel)
        {
            Title = "Notes"
        });


        ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
        {
          var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
          if (confirmed)
          {
            // TODO: Tell the view model, aka BindingContext, to save.
            viewModel.SaveAccountCommand.Execute(null);

          }
          else
          {
            Console.WriteLine("cancel changes!");
          }
        }));

      }  //end ctor



      protected override void OnAppearing()
      {
          base.OnAppearing();

          viewAcctOrders.RefreshView(viewModel.IsInitialized);
          viewAcctHistory.RefreshView(viewModel.IsInitialized);

          if (viewModel.IsInitialized)
          {
              return;
          }

          viewModel.IsInitialized = true;
      }

    } //end class
}
