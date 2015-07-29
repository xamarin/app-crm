using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Sales
{
    public class LeadContactDetailPage : BaseLeadDetailPage
    {
        // NOTE: the ViewModel is contained in the base class

        public LeadContactDetailPage(LeadDetailViewModel viewModel) : base(TextResources.Leads_LeadDetail_SaveButtonText.ToUpper(), viewModel)
        {
            #region roleEntry
            EntryCell roleEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_Role
            };
            roleEntryCell.SetBinding(EntryCell.TextProperty, "Lead.JobTitle", BindingMode.TwoWay);
            #endregion

            #region firstNameEntry
            EntryCell firstNameEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_FirstName
            };
            firstNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.FirstName", BindingMode.TwoWay);
            #endregion

            #region lastNameEntry
            EntryCell lastNameEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_LastName
            };
            lastNameEntryCell.SetBinding(EntryCell.TextProperty, "Lead.LastName", BindingMode.TwoWay);
            #endregion

            #region phoneEntry
            EntryCell phoneEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_Phone,
                Keyboard = Keyboard.Telephone
            };
            phoneEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Phone", BindingMode.TwoWay);
            #endregion

            #region emailEntry
            EntryCell emailEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_Email,
                Keyboard = Keyboard.Email
            };
            emailEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Email", BindingMode.TwoWay);
            #endregion

            #region streetEntry
            EntryCell streetEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_Address
            };
            streetEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Street", BindingMode.TwoWay);
            #endregion

            #region postalCodeEntry
            EntryCell postalCodeEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_PostalCode,
                Keyboard = Keyboard.Numeric
            };
            postalCodeEntryCell.SetBinding(EntryCell.TextProperty, "Lead.PostalCode", BindingMode.TwoWay);
            #endregion

            #region cityEntry
            EntryCell cityEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_City
            };
            cityEntryCell.SetBinding(EntryCell.TextProperty, "Lead.City", BindingMode.TwoWay);
            #endregion

            #region stateEntry
            EntryCell stateEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_State
            };
            stateEntryCell.SetBinding(EntryCell.TextProperty, "Lead.State", BindingMode.TwoWay);
            #endregion

            #region countryEntry
            EntryCell countryEntryCell = new EntryCell()
            {
                BindingContext = ViewModel,
                Label = TextResources.Leads_LeadContactDetail_Country
            };
            countryEntryCell.SetBinding(EntryCell.TextProperty, "Lead.Country", BindingMode.TwoWay);
            #endregion

            #region compose table view
            TableView tableView = new TableView()
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

            // add the table view to the already existing stack layout in the base class
            StackLayout.Children.Add(tableView);
        }
    }
}
