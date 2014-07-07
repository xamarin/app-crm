using MobileCRM.Shared.Models;
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
	public partial class AccountDetailsView
	{
    AccountDetailsViewModel viewModel;
    public AccountDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();

      SetBinding(Page.TitleProperty, new Binding("Title"));
      SetBinding(Page.IconProperty, new Binding("Icon"));

      this.BindingContext = vm;
      ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
      {
        var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
        if (confirmed)
        {
          // TODO: Tell the view model, aka BindingContext, to save.
          viewModel.SaveContactCommand.Execute(null);
          
        }
        else
        {
          Console.WriteLine("cancel changes!");
        }
      }));
		}
	}
}
