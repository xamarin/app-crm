using System;
using System.Diagnostics;
using System.Linq;
using MobileCRM.CustomControls;
using MobileCRM.Helpers;
using MobileCRM.Interfaces;
using MobileCRM.ViewModels.Orders;
using Xamarin;
using Xamarin.Forms;
using MobileCRM.ViewModels.Customers;

namespace MobileCRM.Pages.Accounts
{
    public partial class AccountDetailsView
    {
        CustomerDetailViewModel viewModelAcct;
        OrdersViewModel viewModelOrders;

        public AccountDetailsView(CustomerDetailViewModel vmAcct, OrdersViewModel vmOrders)
        {
            InitializeComponent();
            vmOrders.Account = vmAcct.Account;
            this.BindingContext = viewModelOrders = vmOrders;

            this.Icon = "account.png";
            this.Title = "Account";
        }

        void PopulateChart()
        {
            try
            {
                if (viewModelOrders.Orders.Count() > 0)
                {
                    var barData = new BarGraphHelper(viewModelOrders.Orders, false);

                    var orderedData = (barData.SalesData.OrderBy(data => data.DateStart).Select(data => new BarItem
                        {
                            Name = data.DateStartString,
                            Value = Convert.ToInt32(data.Amount)
                        })).ToList();

                    BarChart.Items = orderedData;
                } //end if

            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                Debug.WriteLine("EXCEPTION: AccountDetailsView.PopulateChart(): " + exc.Message + "  |  " + exc.StackTrace);
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                await viewModelOrders.ExecuteLoadOrdersCommand();

                this.PopulateChart();

                viewModelOrders.IsInitialized = true;

                Insights.Track("Account Details Page");

            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                Debug.WriteLine("EXCEPTION: AccountDetailsView.OnAppearing(): " + exc.Message + "  |  " + exc.StackTrace);
            }
        }

        public void RefreshView()
        {
            this.PopulateChart();
        }

        async void OnPhoneTapped(object sender, EventArgs e)
        {
            string phoneCell = PhoneCell.Detail;

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
}