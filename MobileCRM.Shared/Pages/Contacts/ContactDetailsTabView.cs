using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.Pages.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Contacts
{

    public class ContactDetailsTabView : TabbedPage
    {

        private ContactDetailsViewModel viewModel;

        public ContactDetailsTabView(Contact contact)
        {
            this.Title = contact.DisplayName;
            viewModel = new ContactDetailsViewModel(contact) { Navigation = Navigation };

            this.Children.Add(new ContactDetailsView(viewModel) { Title = "Contact" });

            this.Children.Add(new ContactMapView(viewModel) { Title = "Map" });

            ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
            {
                var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
                if (confirmed)
                {
                    viewModel.SaveContactCommand.Execute(null);
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
