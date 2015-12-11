// The MIT License (MIT)
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
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Sales;

namespace XamarinCRM.Pages.Sales
{
    public class LeadContactDetailPage : ModelBoundContentPage<LeadDetailViewModel>
    {
        public LeadContactDetailPage()
        {
            #region roleEntry
            EntryCell roleEntryCell = new EntryCell()
            {
                Label = TextResources.Leads_LeadContactDetail_Role, 
                    LabelColor = Palette._007,
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
                    new TableSection()
                    {
                        roleEntryCell,
                        firstNameEntryCell,
                        lastNameEntryCell
                    },
                    new TableSection()
                    {
                        phoneEntryCell,
                        emailEntryCell

                    },
                    new TableSection()
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

            Insights.Track(InsightsReportingConstants.PAGE_LEADCONTACTDETAIL);
        }
    }
}
