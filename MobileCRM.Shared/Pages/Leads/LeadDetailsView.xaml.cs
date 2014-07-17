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


		public LeadDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();

			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = vm;



			foreach (var item in Account.IndustryTypes)
			{
					IndustryTypePicker.Items.Add(item);
			}

			foreach (var opt in Account.OpportunityStages)
			{
					OpportunityStagePicker.Items.Add(opt);
			}

			//IndustryTypePicker.SelectedIndex = viewModel.IndustryType;
			//OpportunityStagePicker.SelectedIndex = viewModel.OpptStage;


			//IndustryTypePicker.SelectedIndexChanged += IndustryTypePicker_SelectedIndexChanged;
			//OpportunityStagePicker.SelectedIndexChanged += OpportunityStagePicker_SelectedIndexChanged;

			//OpptSize.TextChanged += OpptSize_TextChanged;


			//CancelButton.Clicked += (sender, args) =>
			//  {
			//    Navigation.PopModalAsync();
			//  };

		} //end ctor

		//void OpptSize_TextChanged(object sender, TextChangedEventArgs e)
		//{
		//    viewModel.OpportunitySize = OpptSize.Text;
		//}

		//void OpportunityStagePicker_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    viewModel.OpptStage = OpportunityStagePicker.SelectedIndex;
		//}

		//void IndustryTypePicker_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    viewModel.IndustryType = IndustryTypePicker.SelectedIndex;
		//}

	}
}
