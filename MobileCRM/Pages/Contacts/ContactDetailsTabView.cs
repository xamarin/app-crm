using MobileCRM.Models;
using MobileCRM.ViewModels.Contacts;
using MobileCRM.Pages.Contacts;
using System;
using Xamarin.Forms;

namespace MobileCRM.Pages.Contacts
{
    public class ContactDetailsTabView : TabbedPage
    {
        private ContactDetailsViewModel viewModel;

        public ContactDetailsTabView(Contact contact)
        {
            if (contact != null)
            {
                this.Title = contact.DisplayName;
            }
            else
            {
                this.Title = "New Contact";
            }

            viewModel = new ContactDetailsViewModel(contact) { Navigation = Navigation };

            this.Children.Add(new ContactDetailsView(viewModel) { Title = "Contact" });

            this.Children.Add(new ContactMapView(viewModel) { Title = "Map" });

            ToolbarItems.Add(new ToolbarItem("Done", "save.png", async () =>
                    {
                        var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
                        if (confirmed)
                        {
                            viewModel.SaveContactCommand.Execute(null);
                        }
                        else
                        {
                            viewModel.GoBack();
                            System.Diagnostics.Debug.WriteLine("ContactDetailsTabView - cancel changes!");
                        }
                    }));
        }
        //end ctor
    }
    //end class
}
//end ns