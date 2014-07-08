using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Leads;
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
    LeadDetailsViewModel viewModel;
		public LeadDetailsView (Account account)
		{
			InitializeComponent ();
      this.BindingContext = this.viewModel = new LeadDetailsViewModel(Navigation, account);

      CancelButton.Clicked += (sender, args) =>
        {
          Navigation.PopModalAsync();
        };
		}
	}
}
