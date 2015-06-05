using MobileCRM.Shared.ViewModels.Accounts;
using System;
using MobileCRM.Shared.Interfaces;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Shared.Pages.Accounts
{
    public partial class AccountContactView
    {
        public AccountContactView(AccountDetailsViewModel vm)
        {
            InitializeComponent();

            SetBinding(Page.TitleProperty, new Binding("Title"));

            this.Icon = "contact.png";

            this.BindingContext = vm;

            Insights.Track("Contact Page");

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
 //end ns
