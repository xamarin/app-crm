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
        this.Title = account.DisplayName;
        viewModel = new AccountDetailsViewModel(Navigation, account);
        this.Children.Add(new AccountDetailsView(viewModel));
        this.Children.Add(new AccountDetailsMapView(viewModel));

      }
    }
}
