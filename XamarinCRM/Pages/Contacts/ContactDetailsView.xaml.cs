using System;
using MobileCRM.Interfaces;
using MobileCRM.ViewModels.Contacts;
using Xamarin;
using Xamarin.Forms;

namespace MobileCRM.Pages.Contacts
{
    public partial class ContactDetailsView
    {
        public ContactDetailsView(ContactDetailsViewModel viewModel)
        {
            InitializeComponent();

            SetBinding(TitleProperty, new Binding("Title"));
            SetBinding(IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel;

            Insights.Track("Contact Details Page");
        }
        //end ctor

        async void OnPhoneTapped(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            string phoneCell = ((EntryCell)sender).Text;

            if (String.IsNullOrEmpty(phoneCell) == true)
            {
                return;
            }            

            if (await this.DisplayAlert(
                 "Dial a Number",
                 "Would you like to call " + phoneCell + "?",
                 "Yes",
                 "No"))
            {

                var dialer = DependencyService.Get<IDialer>();
                phoneCell = phoneCell.Replace("-", "");
                if (dialer == null)
                {
                    return;
                }

                dialer.Dial(phoneCell);
            }
        }
    }
    //end class
}