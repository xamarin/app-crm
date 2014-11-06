using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Contacts
{
	public partial class ContactsView
	{
	private ContactsViewModel ViewModel
	{
		get { return BindingContext as ContactsViewModel; }
	}

	public ContactsView(ContactsViewModel vm)
		{
			InitializeComponent ();

				this.BindingContext = vm;

				ToolbarItems.Add(new ToolbarItem
				{
				Icon = "add.png",
				Name = "add",
				Command = new Command(() =>
				{
						//navigate to new page
						Navigation.PushAsync(new ContactDetailsTabView(null));

				})
				});

				//SYI: Removed - Pull contacts only on initial load.  
				//ToolbarItems.Add(new ToolbarItem
				//{
				//Icon = "refresh.png",
				//Name = "refresh",
				//Command = ViewModel.LoadContactsCommand
				//});

		}

	public void OnItemSelected(object sender, ItemTappedEventArgs e)
	{
		if (e.Item == null)
		return;

		Navigation.PushAsync(new ContactDetailsTabView(e.Item as Contact));

		ContactList.SelectedItem = null;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (ViewModel.IsInitialized)
		{
		return;
		}
		ViewModel.LoadContactsCommand.Execute(null);
		ViewModel.IsInitialized = true;
	}
	}
}
