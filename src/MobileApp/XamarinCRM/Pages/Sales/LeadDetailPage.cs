using XamarinCRM.Models;
using Xamarin.Forms;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Converters;
using XamarinCRM.Pages.Base;
using XamarinCRM.Cells;

namespace XamarinCRM.Pages.Sales
{
    public class LeadDetailPage : ModelBoundContentPage<LeadDetailViewModel>
    {
        public LeadDetailPage()
        {
            #region name entry
            EntryCell companyNameEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadDetail_CompanyName, 
                Placeholder = TextResources.Leads_LeadDetail_CompanyNamePlaceholder
            };
            companyNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Company", BindingMode.TwoWay);
            #endregion

            #region industry picker
            PickerCell industryPickerCell = new PickerCell();
            industryPickerCell.Picker.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            industryPickerCell.Picker.Title = TextResources.Leads_LeadDetail_Industry;
            foreach (var industry in Account.IndustryTypes)
            {
                industryPickerCell.Picker.Items.Add(industry);
            }
            industryPickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "Lead.IndustryTypeCurrentIndex", BindingMode.TwoWay);
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
                Keyboard = Keyboard.Numeric
            };
            opportunitySizeEntryCell.SetBinding(EntryCell.TextProperty, "Lead.OpportunitySize", BindingMode.TwoWay, new CurrencyDoubleConverter());
            #endregion

            #region opportunity stage picker
            PickerCell opportunityStagePickerCell = new PickerCell();
            opportunityStagePickerCell.Picker.HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            opportunityStagePickerCell.Picker.Title = TextResources.Leads_LeadDetail_OpportunityStage;
            foreach (var opportunityStage in Account.OpportunityStages)
            {
                opportunityStagePickerCell.Picker.Items.Add(opportunityStage);
            }
            opportunityStagePickerCell.Picker.SetBinding(Picker.SelectedIndexProperty, "Lead.OpportunityStageCurrentIndex", BindingMode.TwoWay);
            opportunityStagePickerCell.Picker.SelectedIndexChanged += (sender, e) =>
            {
                ViewModel.Lead.OpportunityStage = opportunityStagePickerCell.Picker.Items[opportunityStagePickerCell.Picker.SelectedIndex];
            };
            #endregion

            #region compose table view
            Content = new TableView()
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
        }
    }
}
