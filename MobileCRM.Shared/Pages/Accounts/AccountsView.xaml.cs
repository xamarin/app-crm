using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Shared.Pages.Accounts
{
    public partial class AccountsView
    {
        AccountsViewModel viewModel;

        public AccountsView(AccountsViewModel viewModel)
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