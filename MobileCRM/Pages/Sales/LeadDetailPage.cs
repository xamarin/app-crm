using MobileCRM.Models;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailPage : BaseLeadDetailPage
    {
        // NOTE: the ViewModel is contained in the base class

        public LeadDetailPage(LeadDetailViewModel viewModel) : base(TextResources.Leads_LeadDetail_SaveButtonText.ToUpper(), viewModel)
        {
            #region name entry
            EntryCell companyNameEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadDetail_CompanyName, 
                Placeholder = TextResources.Leads_LeadDetail_CompanyNamePlaceholder,
                BindingContext = ViewModel.Lead
            };
            companyNameEntryCell.SetBinding(EntryCell.TextProperty, "Company", BindingMode.TwoWay);
            #endregion

            #region industry picker
            PickerCell industryPickerCell = new PickerCell();
            industryPickerCell.Picker.BindingContext = ViewModel.Lead;
            industryPickerCell.Picker.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            industryPickerCell.Picker.Title = TextResources.Leads_LeadDetail_Industry;
            foreach (var industry in Account.IndustryTypes)
            {
                industryPickerCell.Picker.Items.Add(industry);
            }
            industryPickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "IndustryTypeCurrentIndex", BindingMode.TwoWay);
            industryPickerCell.Picker.SelectedIndexChanged += (sender, e) =>
            {
                ViewModel.Lead.Industry = industryPickerCell.Picker.Items[industryPickerCell.Picker.SelectedIndex];
            };
            #endregion

            #region opportunity size entry
            EntryCell opportunitySizeEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadDetail_OpportunitySize, 
                Placeholder = TextResources.Leads_LeadDetail_OpportunitySizePlaceholder,
                BindingContext = ViewModel.Lead,
                Keyboard = Keyboard.Numeric
            };
            opportunitySizeEntryCell.SetBinding(EntryCell.TextProperty, "OpportunitySize", BindingMode.TwoWay, new CurrencyConverter());
            #endregion

            #region opportunity stage picker
            PickerCell opportunityStagePickerCell = new PickerCell();
            opportunityStagePickerCell.Picker.BindingContext = ViewModel.Lead;
            opportunityStagePickerCell.Picker.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            opportunityStagePickerCell.Picker.Title = TextResources.Leads_LeadDetail_OpportunityStage;
            foreach (var opportunityStage in Account.OpportunityStages)
            {
                opportunityStagePickerCell.Picker.Items.Add(opportunityStage);
            }
            opportunityStagePickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "OpportunityStageCurrentIndex", BindingMode.TwoWay);
            opportunityStagePickerCell.Picker.SelectedIndexChanged += (sender, e) =>
            {
                    ViewModel.Lead.OpportunityStage = opportunityStagePickerCell.Picker.Items[opportunityStagePickerCell.Picker.SelectedIndex];
            };
            #endregion

            #region compose table view
            TableView tableView = new TableView()
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot()
                {
                    new TableSection()
                    {
                        companyNameEntryCell,
                        industryPickerCell
                    },
                    new TableSection(TextResources.Leads_LeadDetail_OpportunityHeading)
                    {
                        opportunitySizeEntryCell,
                        opportunityStagePickerCell
                    }
                }
            };
            #endregion

            // add the table view to the already existing stack layout in the base class
            StackLayout.Children.Add(tableView);
        }
    }
}
