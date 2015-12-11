//
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Cells;
using XamarinCRM.Converters;
using XamarinCRM.Models;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;

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
                Placeholder = TextResources.Leads_LeadDetail_CompanyNamePlaceholder,
                Keyboard = Keyboard.Text
            };
            companyNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Company", BindingMode.TwoWay);
            #endregion

            #region industry picker
            PickerCell industryPickerCell = new PickerCell();
            industryPickerCell.Label.Text = TextResources.Leads_LeadDetail_Industry;
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
            opportunityStagePickerCell.Label.Text = TextResources.Leads_LeadDetail_OpportunityStage;
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

            #region roleEntry
            EntryCell roleEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Role, LabelColor = Palette._007,
                Keyboard = Keyboard.Text
            };
            roleEntryCell.SetBinding(EntryCell.TextProperty, "Lead.JobTitle", BindingMode.TwoWay);
            #endregion

            #region firstNameEntry
            EntryCell firstNameEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_FirstName,
                Keyboard = Keyboard.Text
            };
            firstNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.FirstName", BindingMode.TwoWay);
            #endregion

            #region lastNameEntry
            EntryCell lastNameEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_LastName,
                Keyboard = Keyboard.Text
            };
            lastNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.LastName", BindingMode.TwoWay);
            #endregion

            #region phoneEntry
            EntryCell phoneEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Phone,
                Keyboard = Keyboard.Telephone
            };
            phoneEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Phone", BindingMode.TwoWay);
            #endregion

            #region emailEntry
            EntryCell emailEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Email,
                Keyboard = Keyboard.Email
            };
            emailEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Email", BindingMode.TwoWay);
            #endregion

            #region streetEntry
            EntryCell streetEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Address,
                Keyboard = Keyboard.Text
            };
            streetEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Street", BindingMode.TwoWay);
            #endregion

            #region postalCodeEntry
            EntryCell postalCodeEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_PostalCode,
                Keyboard = Keyboard.Numeric
            };
            postalCodeEntryCell.SetBinding(EntryCell.TextProperty, "Lead.PostalCode", BindingMode.TwoWay);
            #endregion

            #region cityEntry
            EntryCell cityEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_City,
                Keyboard = Keyboard.Text
            };
            cityEntryCell.SetBinding(EntryCell.TextProperty, "Lead.City", BindingMode.TwoWay);
            #endregion

            #region stateEntry
            EntryCell stateEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_State,
                Keyboard = Keyboard.Text
            };
            stateEntryCell.SetBinding(EntryCell.TextProperty, "Lead.State", BindingMode.TwoWay);
            #endregion

            #region countryEntry
            EntryCell countryEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Country,
                Keyboard = Keyboard.Text
            };
            countryEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Country", BindingMode.TwoWay);
            #endregion

            #region compose view hierarchy

            Content = new TableView()
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot()
                {
                    new TableSection("Company")
                    {
                        companyNameEntryCell,
                        industryPickerCell
                    },
                    new TableSection(TextResources.Leads_LeadDetail_OpportunityHeading)
                    {
                        opportunitySizeEntryCell,
                        opportunityStagePickerCell
                    },
                    new TableSection("Info")
                    {
                        roleEntryCell,
                        firstNameEntryCell,
                        lastNameEntryCell
                    },
                    new TableSection("Contact")
                    {
                        phoneEntryCell,
                        emailEntryCell

                    },
                    new TableSection("Address")
                    {
                        streetEntryCell,
                        postalCodeEntryCell,
                        cityEntryCell,
                        stateEntryCell,
                        countryEntryCell
                    }
                }
            };

            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track(InsightsReportingConstants.PAGE_LEADDETAIL);
        }
    }
}
