using System.Diagnostics;
using MobileCRM.Models;
using MobileCRM.Pages.Accounts;
using MobileCRM.ViewModels.Accounts;
using Xamarin.Forms;

namespace MobileCRM.Pages.Leads
{
    public class LeadDetailsTabView : TabbedPage
    {
        AccountDetailsViewModel viewModel;

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

            this.Children.Add(new LeadDetailsView(viewModel)
                {
                    Title = "Company"
                });
            this.Children.Add(new AccountContactView(viewModel)
                {
                    Title = "Contact"
                });

            this.Children.Add(new AccountMapView(viewModel)
                {

                    Title = "Map"
                });

            ToolbarItems.Add(new ToolbarItem("Done", "save.png", async () =>
                    {
                        var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
                        if (confirmed)
                        {
                            viewModel.SaveAccountCommand.Execute(null);
                        }
                        else
                        {
                            viewModel.GoBack();
                            Debug.WriteLine("cancel changes!");
                        }
                    }));
        }
        //end ctor
    }
    //end class
}
//end ns