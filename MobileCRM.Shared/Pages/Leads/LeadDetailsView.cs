using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Leads
{
    public class LeadDetailsView : BaseView
    {
        AccountDetailsViewModel viewModel;

        public LeadDetailsView(AccountDetailsViewModel vm)
        {

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;

            this.Content = this.BuildView();

        } //end ctor


        private StackLayout BuildView()
        {
            this.BackgroundColor = AppColors.CONTENTLIGHTBKG;

            Label lblCompany = new Label() { Text = "Company Name: ", TextColor = AppColors.LABELBLUE };
            Entry entryCompany = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = AppColors.LABELGRAY };
            entryCompany.SetBinding(Entry.TextProperty, "Account.Company");

            Label lblIndustry = new Label() { Text = "Industry: ", TextColor = AppColors.LABELBLUE };
            Picker pickerIndustry = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = AppColors.LABELGRAY };
            
            //Populate Industry Picker
            foreach (var i in Account.IndustryTypes)
            {
                pickerIndustry.Items.Add(i);
            }
            pickerIndustry.SetBinding(Picker.SelectedIndexProperty, "IndustryIndex");


            Label lblOpptSize = new Label() { Text = "Opportunity Size $: ", TextColor = AppColors.LABELBLUE };
            Entry entryOpptSize = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric, 
                BackgroundColor = AppColors.LABELGRAY };
            entryOpptSize.SetBinding(Entry.TextProperty, "OpportunitySize");

            Label lblOpptStage = new Label() { Text = "Opportunity Stage: ", TextColor = AppColors.LABELBLUE };
           
            Picker pickerOpptStage = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = AppColors.LABELGRAY};

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