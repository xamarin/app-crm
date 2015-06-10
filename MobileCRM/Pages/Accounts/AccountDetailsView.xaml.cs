using MobileCRM.CustomControls;
using MobileCRM.Helpers;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using MobileCRM.Interfaces;
using Xamarin.Forms;
using Xamarin;
using MobileCRM.CustomControls;

namespace MobileCRM.Pages.Accounts
{
    public partial class AccountDetailsView
    {
        AccountDetailsViewModel viewModelAcct;
        OrdersViewModel viewModelOrders;

        public AccountDetailsView(AccountDetailsViewModel vmAcct, OrdersViewModel vmOrders)
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

                    var orderedData = (from data in barData.SalesData
                                            orderby data.DateStart
                                            select new BarItem
                    {
                        Name = data.DateStartString,
                        Value = Convert.ToInt32(data.Amount)
                    }).ToList();

                    BarChart.Items = orderedData;
                } //end if

            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                System.Diagnostics.Debug.WriteLine("EXCEPTION: AccountDetailsView.PopulateChart(): " + exc.Message + "  |  " + exc.StackTrace);
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
                System.Diagnostics.Debug.WriteLine("EXCEPTION: AccountDetailsView.OnAppearing(): " + exc.Message + "  |  " + exc.StackTrace);
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