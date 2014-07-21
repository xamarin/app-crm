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


    } //end class
} //end ns