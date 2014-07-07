using MobileCRM.Shared.Models;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountsView
	{
    private AccountsViewModel ViewModel
    {
      get { return BindingContext as AccountsViewModel; }
    }

    public AccountsView(AccountsViewModel vm)
		{
			InitializeComponent ();

      this.BindingContext = vm;

      ToolbarItems.Add(new ToolbarItem
      {
        Icon = "refresh.png",
        Name = "refresh",
        Command = ViewModel.LoadAccountsCommand
      });

		}

    public void OnItemSelected(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      Navigation.PushAsync(new AccountDetailsTabView(e.Item as Account));

      ContactList.SelectedItem = null;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      if (ViewModel.IsInitialized)
      {
        return;
      }
      ViewModel.LoadAccountsCommand.Execute(null);
      ViewModel.IsInitialized = true;
    }
	}
}
