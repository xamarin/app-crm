using MobileCRM.Models;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailPage : BaseLeadDetailPage
    {
        // NOTE: the ViewModel is contained in the base class

        public LeadDetailPage(LeadDetailViewModel viewModel)
            : base(TextResources.Leads_LeadDetail_SaveButtonText.ToUpper(), viewModel)
        {
            #region name entry
            EntryCell companyNameEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadDetail_Name, 
                Placeholder = TextResources.Leads_LeadDetail_NamePlaceholder,
                BindingContext = ViewModel
            };
            companyNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Company", BindingMode.TwoWay);
            #endregion

            #region industry picker
            PickerCell industryPickerCell = new PickerCell();
            industryPickerCell.Picker.Title = TextResources.Leads_LeadDetail_Industry;
            foreach (var industry in Account.IndustryTypes)
            {
                industryPickerCell.Picker.Items.Add(industry);
            }
            industryPickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "Lead.IndustryTypeCurrentIndex");
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
                BindingContext = ViewModel
            };
            opportunitySizeEntryCell.SetBinding(EntryCell.TextProperty, "Lead.OpportunitySize", BindingMode.TwoWay);
            #endregion

            #region opportunity stage picker
            PickerCell opportunityStagePickerCell = new PickerCell();
            opportunityStagePickerCell.Picker.Title = TextResources.Leads_LeadDetail_OpportunityStage;
            foreach (var opportunityStage in Account.OpportunityStages)
            {
                opportunityStagePickerCell.Picker.Items.Add(opportunityStage);
            }
            opportunityStagePickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "Lead.OpportunityStageCurrentIndex");
            opportunityStagePickerCell.Picker.SelectedIndexChanged += (sender, e) =>
            {
                ViewModel.Lead.Industry = opportunityStagePickerCell.Picker.Items[opportunityStagePickerCell.Picker.SelectedIndex];
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
