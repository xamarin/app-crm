using MobileCRM.Models;
using MobileCRM.ViewModels.Orders;
using System;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Pages.Orders
{
    public partial class AccountOrdersView
    {
        OrdersViewModel viewModel;
        string accountId;

        public AccountOrdersView(string acctId, OrdersViewModel vm)
        {
            InitializeComponent();
            this.accountId = acctId;
            this.BindingContext = this.viewModel = vm;

        }

        public void NewOrderClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewOrderView(accountId));
        }

        public void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            
            Navigation.PushModalAsync(new AccountOrderDetailsView(e.Item as Order));

            OrdersList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //if (viewModel.IsInitialized)
            //{
            //  return;
            //}
            viewModel.LoadOrdersCommand.Execute(null);
            viewModel.IsInitialized = true;

            Insights.Track("Account Details Orders Page");
        }

        public void RefreshView()
        {

            viewModel.LoadOrdersCommand.Execute(null);
            viewModel.IsInitialized = true;
        }
    }
}