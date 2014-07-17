using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Leads;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.Pages.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Leads
{
    public class LeadDetailsTabView : TabbedPage
    {
        //private LeadDetailsViewModel viewModel;
        private AccountDetailsViewModel viewModel;

        public LeadDetailsTabView(Account l)
        {
            if (l != null)
            {
                this.Title = l.Company;
            }
            else
            {
                this.Title = "New Lead";
            }
            


            Account lead = l == null ? new Account() { IsLead = true } : l;

            
            viewModel = new AccountDetailsViewModel(lead) { Navigation = Navigation };

            //this.Children.Add(new LeadDetailsView(viewModel)
            this.Children.Add(new LeadDetailsView2(viewModel)
                {
                    Title = "Lead"
                });
            this.Children.Add(new AccountContactView(viewModel) 
            {
                Title = "Contact"
            });

            this.Children.Add(new AccountMapView(viewModel)
            {

                Title = "Map"
            });


            ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
            {
                var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
                if (confirmed)
                {
                    // TODO: Tell the view model, aka BindingContext, to save.
                    viewModel.SaveAccountCommand.Execute(null);

                }
                else
                {
                    viewModel.GoBack();
                    Console.WriteLine("cancel changes!");
                }
            }));
            

        } //end ctor




    } //end class

} //end ns
