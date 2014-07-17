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

      public AccountDetailsTabView(Account account)
      {
        
          if (account != null)
          {
              this.Title = account.DisplayName;
          }
          else
          {
              this.Title = "New Lead";
          }
          
          



        viewModel = new AccountDetailsViewModel(account) { Navigation = Navigation };
        this.Children.Add(new AccountDetailsView(viewModel));
        this.Children.Add(new AccountOrdersView(account.Id)
        {
          Title = "Orders"
        });

        this.Children.Add(new AccountHistoryView(account.Id)
        {
          Title = "History"
        });


        this.Children.Add(new AccountNotesView(account)
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

      }
    }
}
