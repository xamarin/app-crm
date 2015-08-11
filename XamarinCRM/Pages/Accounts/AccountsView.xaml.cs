using MobileCRM.Models;
using Xamarin;
using Xamarin.Forms;
using MobileCRM.ViewModels.Customers;

namespace MobileCRM.Pages.Accounts
{
    public partial class AccountsView
    {
        CustomersViewModel viewModel;

        public AccountsView(CustomersViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = this.viewModel = viewModel;
        }

        public void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var page = new AccountDetailsTabView(e.Item as Account);
            Navigation.PushAsync(page);

            ContactList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track("Accounts Page");

            if (viewModel.IsInitialized)
            {
                return;
            }
            viewModel.LoadAccountsCommand.Execute(null);
            viewModel.IsInitialized = true;
        }
    }
}