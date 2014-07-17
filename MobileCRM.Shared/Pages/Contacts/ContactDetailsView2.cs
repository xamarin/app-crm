using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Contacts
{
    public class ContactDetailsView2 : BaseView
    {
        ContactDetailsViewModel viewModel;

        public ContactDetailsView2(ContactDetailsViewModel vm)
        {

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;


            //ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
            //{
            //    var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
            //    if (confirmed)
            //    {
            //        // TODO: Tell the view model, aka BindingContext, to save.
            //        viewModel.SaveContactCommand.Execute(null);
            //    }
            //    else
            //    {
            //        Console.WriteLine("cancel changes!");
            //    }
            //}));

            this.Content = this.BuildView();
        } //end ctor


        private TableView BuildView()
        {
            EntryCell entryCompany = new EntryCell() { Label = "Company" };
            entryCompany.SetBinding(EntryCell.TextProperty, "Contact.Company");

            EntryCell entryTitle = new EntryCell() { Label = "Title" };
            entryTitle.SetBinding(EntryCell.TextProperty, "Contact.Title");


            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection("Name & Title")
                    {
                        //new EntryCell
                        //{
                        //    Label = "Company",
                        //    Text = "",
                        //    Placeholder = "Type Text Here"
                        //},

                        entryCompany,

                        //new EntryCell
                        //{
                        //    Label = "Title",
                        //    Text = "",
                        //    Placeholder = ""
                        //},

                        entryTitle,

                        new EntryCell
                        {
                            Label = "First Name",
                            Text = "",
                            Placeholder = ""
                        },

                        new EntryCell
                        {
                            Label = "Last Name",
                            Text = "",
                            Placeholder = ""
                        },
                    }, //Name & Title

                    new TableSection("Contact Info")
                    {
                        new EntryCell
                        {
                            Label = "Phone",
                            Text = "",
                            Placeholder = "",
                            Keyboard = Keyboard.Telephone
                        },
                        new EntryCell
                        {
                            Label = "Email",
                            Text = "",
                            Placeholder = "",
                            Keyboard = Keyboard.Email
                        },
                     
                        //TODO: Call Phone, Send Email buttons

                    }, //end Contact Info

                    new TableSection("Address")
                    {
                        new EntryCell
                        {
                            Label = "Street",
                            Text = "",
                            Placeholder = ""
                        },
                        new EntryCell
                        {
                            Label = "City",
                            Text = "",
                            Placeholder = ""
                        },
                        new EntryCell
                        {
                            Label = "State",
                            Text = "",
                            Placeholder = ""
                        },
                        new EntryCell
                        {
                            Label = "Postal Code",
                            Text = "",
                            Placeholder = "",
                            Keyboard = Keyboard.Numeric
                        }
                     
                    } //end Address


                } //TableRoot
            };

            // Accomodate iPhone status bar.
            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            return tableView;
        } //end BuildView

    } //end class
} //end ns
