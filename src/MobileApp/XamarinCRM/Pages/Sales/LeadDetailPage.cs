//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using Xamarin.Forms;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Converters;
using XamarinCRM.Pages.Base;
using XamarinCRM.Cells;
using XamarinCRM.Statics;
using XamarinCRM.Models;
using Xamarin;

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

            #region iOS Specific TableView
            if(Device.OS == TargetPlatform.iOS)
            {
                #region roleEntry
                EntryCell roleEntryCell = new EntryCell()
                    {
                        Label = TextResources.Leads_LeadContactDetail_Role, LabelColor = Palette._007
                    };
                roleEntryCell.SetBinding(EntryCell.TextProperty, "Lead.JobTitle", BindingMode.TwoWay);
                #endregion

                #region firstNameEntry
                EntryCell firstNameEntryCell = new EntryCell()
                    {
                        Label = TextResources.Leads_LeadContactDetail_FirstName
                    };
                firstNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.FirstName", BindingMode.TwoWay);
                #endregion

                #region lastNameEntry
                EntryCell lastNameEntryCell = new EntryCell()
                    {
                        Label = TextResources.Leads_LeadContactDetail_LastName
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
                        Label = TextResources.Leads_LeadContactDetail_Address
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
                        Label = TextResources.Leads_LeadContactDetail_City
                    };
                cityEntryCell.SetBinding(EntryCell.TextProperty, "Lead.City", BindingMode.TwoWay);
                #endregion

                #region stateEntry
                EntryCell stateEntryCell = new EntryCell()
                    {
                        Label = TextResources.Leads_LeadContactDetail_State
                    };
                stateEntryCell.SetBinding(EntryCell.TextProperty, "Lead.State", BindingMode.TwoWay);
                #endregion

                #region countryEntry
                EntryCell countryEntryCell = new EntryCell()
                    {
                        Label = TextResources.Leads_LeadContactDetail_Country
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

            #endregion

            #region compose table view
            if(Device.OS != TargetPlatform.iOS)
            {
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
            }
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track(InsightsReportingConstants.PAGE_LEADDETAIL);
        }
    }
}
