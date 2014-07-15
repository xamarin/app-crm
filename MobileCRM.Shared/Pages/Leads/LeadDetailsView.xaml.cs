using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Leads;
using MobileCRM.Shared.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Leads
{
	public partial class LeadDetailsView
	{
		//LeadDetailsViewModel viewModel;
			AccountDetailsViewModel viewModel;
      public LeadDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();

			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = vm;

			CancelButton.Clicked += (sender, args) =>
				{
					Navigation.PopModalAsync();
				};
		}
	}
}
