using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Leads
{
	public partial class NewLeadView
	{
		NewLeadViewModel viewModel;
		public NewLeadView()
		{
			InitializeComponent ();

			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = viewModel = new NewLeadViewModel(Navigation);
			ToolbarItems.Add(new ToolbarItem("Done", Device.OnPlatform<string>(null, null, "done.png"), async () =>
			{
				var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
				if (confirmed)
				{
					// TODO: Tell the view model, aka BindingContext, to save.
					viewModel.SaveLeadCommand.Execute(null);
					
				}
				else
				{
					Console.WriteLine("cancel changes!");
					Navigation.PopAsync();
				}
			}));

			foreach (var item in Account.IndustryTypes)
			{
				IndustryTypePicker.Items.Add(item);
			}

			IndustryTypePicker.SelectedIndex = viewModel.IndustryType;

		}
	}
}
