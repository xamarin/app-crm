using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Leads;
using MobileCRM.Shared.Pages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Leads
{
	public partial class LeadsView
	{
		LeadsViewModel viewModel;

		public LeadsView(LeadsViewModel vm)
		{
			InitializeComponent ();

			this.BindingContext = this.viewModel = vm;

			ToolbarItems.Add(new ToolbarItem
			{
				Icon = "add.png",
				Name = "add",
				Command = new Command(() =>
				{
					//navigate to new page
					Navigation.PushAsync(new NewLeadView());
				})
			});

			ToolbarItems.Add(new ToolbarItem
			{
				Icon = "refresh.png",
				Name = "refresh",
				Command = viewModel.LoadLeadsCommand
			});

		}

		public void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			Navigation.PushAsync(new LeadDetailsTabView(e.Item as Account));

			LeadsList.SelectedItem = null;
			
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (viewModel.IsInitialized)
			{
				return;
			}
			viewModel.LoadLeadsCommand.Execute(null);
			viewModel.IsInitialized = true;

		}
	}
}
