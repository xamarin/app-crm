using Xamarin.Forms;
using XamarinCRM.ViewModels.Sales;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;

namespace XamarinCRM.Pages.Sales
{
    public class LeadContactDetailPage : ModelBoundContentPage<LeadDetailViewModel>
    {
        // NOTE: the ViewModel is contained in the base class

        public LeadContactDetailPage()
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
    }
}
