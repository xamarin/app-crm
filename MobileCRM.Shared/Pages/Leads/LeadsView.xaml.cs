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

    private LeadsViewModel ViewModel
    {
        get { return BindingContext as LeadsViewModel; }
    }


		public LeadsView(LeadsViewModel vm)
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
            Navigation.PushAsync(new LeadDetailsTabView(null));

				})
			});

      //ToolbarItems.Add(new ToolbarItem
      //{
      //  Icon = "refresh.png",
      //  Name = "refresh",
      //  Command = ViewModel.LoadLeadsCommand
      //});

		}

		public void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

      try
      {
          Account acct = (Account)e.Item;
          Navigation.PushAsync(new LeadDetailsTabView(acct));

          LeadsList.SelectedItem = null;
      } catch (Exception ex)
      {
          System.Diagnostics.Debug.WriteLine(ex.Message);
      } //end catch
			
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (ViewModel.IsInitialized)
			{
				return;
			}
			ViewModel.LoadLeadsCommand.Execute(null);
			ViewModel.IsInitialized = true;

		}
	}
}
