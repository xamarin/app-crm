using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Leads
{
    public class LeadDetailsView2 : BaseView
    {
        AccountDetailsViewModel viewModel;

        public LeadDetailsView2(AccountDetailsViewModel vm)
        {

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;


            //Test statement
            //viewModel.Account.Company = "Hi There";

            this.Content = this.BuildView();

        } //end ctor


        private StackLayout BuildView()
        {
            Label lblCompany = new Label() { Text = "Company Name: " };
            Entry entryCompany = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };
            entryCompany.SetBinding(Entry.TextProperty, "Account.Company");

            Label lblIndustry = new Label() { Text = "Industry: " };
            Picker pickerIndustry = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand };
            
            //Populate Industry Picker
            foreach (var i in Account.IndustryTypes)
            {
                pickerIndustry.Items.Add(i);
            }
            pickerIndustry.SetBinding(Picker.SelectedIndexProperty, "IndustryIndex");


            Label lblOpptSize = new Label() { Text = "Opportunity Size $: " };
            Entry entryOpptSize = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric };
            entryOpptSize.SetBinding(Entry.TextProperty, "OpportunitySize");

            Label lblOpptStage = new Label() { Text = "Opportunity Stage: " };
            Picker pickerOpptStage = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand };

            //Populate Oppt Stage Picker
            foreach (var o in Account.OpportunityStages)
            {
                pickerOpptStage.Items.Add(o);
            }
            pickerOpptStage.SetBinding(Picker.SelectedIndexProperty, "OpptStageIndex");


            StackLayout stack = new StackLayout()
            {
                Padding = 10, 

                Children = 
                { 
                    lblCompany, 
                    entryCompany,

                    lblIndustry,
                    pickerIndustry,

                    lblOpptSize,
                    entryOpptSize,

                    lblOpptStage,
                    pickerOpptStage

                }
            };

            return stack;
        } //end BuildView


        private TableView BuildTableView()
        {

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection("Company Info")
                    {
                        new EntryCell
                        {
                            Label = "Company",
                            Text = "",
                            Placeholder = "Type Text Here"
                        },

                        new EntryCell
                        {
                            Label = "Title",
                            Text = "",
                            Placeholder = ""
                        },

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