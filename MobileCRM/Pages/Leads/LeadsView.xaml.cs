using MobileCRM.Models;
using MobileCRM.ViewModels.Leads;
using System;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Pages.Leads
{
    public partial class LeadsView
    {
        LeadsViewModel ViewModel
        {
            get { return BindingContext as LeadsViewModel; }
        }
            
        public LeadsView(LeadsViewModel vm)
        {
            InitializeComponent();

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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } //end catch
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Leads List Page");

            if (ViewModel.IsInitialized)
            {
                return;
            }
            ViewModel.LoadLeadsCommand.Execute(null);
            ViewModel.IsInitialized = true;
        }
    }
}