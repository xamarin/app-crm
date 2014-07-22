using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Orders;
using MobileCRM.CustomControls;
using MobileCRM.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Accounts
{

    public class AccountDetailsView2 : BaseView
    {
        AccountDetailsViewModel viewModel;
        //OrdersViewModel ordersviewModel;

        CustomControls.BarChart barChart;

        public AccountDetailsView2(AccountDetailsViewModel vm)
        {

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;

            //ordersviewModel = new OrdersViewModel(false, vm.Account.Id);

            this.Content = this.BuildView();
        }


        private Layout BuildView()
        {
            TableSection sectionInfo = new TableSection("COMPANY INFO");

            TextCell cellCompany = new TextCell() { Text = "Company" };
            cellCompany.SetBinding(TextCell.DetailProperty, "Account.Company");
            sectionInfo.Add(cellCompany);

            TextCell cellIndustry= new TextCell() { Text = "Industry" };
            cellIndustry.SetBinding(TextCell.DetailProperty, "Account.Industry");
            sectionInfo.Add(cellIndustry);

            TextCell cellContact = new TextCell() { Text = "Contact" };
            cellContact.SetBinding(TextCell.DetailProperty, "DisplayContact");
            sectionInfo.Add(cellContact);

            TextCell cellPhone = new TextCell() { Text = "Phone" };
            cellPhone.SetBinding(TextCell.DetailProperty, "Account.Phone");
            sectionInfo.Add(cellPhone);

            TextCell cellAddr = new TextCell() { Text = "Address" };
            cellAddr.SetBinding(TextCell.DetailProperty, "Account.AddressString");
            sectionInfo.Add(cellAddr);


            TableSection sectionSales = new TableSection("SALES");

            barChart = new CustomControls.BarChart() { HeightRequest = 200 };

            this.InitChart();

            StackLayout stackChart = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 10,
                Children = { barChart }
            };

            ViewCell viewChart = new ViewCell() { View = stackChart };
            sectionSales.Add(viewChart);

            TableView tblMain = new TableView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                Root = new TableRoot()
                { 
                    sectionInfo,
                    sectionSales
                }
            };


            ActivityIndicator activityBusy = new ActivityIndicator();
            activityBusy.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            activityBusy.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            StackLayout stack = new StackLayout()
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    activityBusy,
                    tblMain
                }
            };

            ScrollView scroll = new ScrollView() { Content = stack };

            return scroll;
        } //end BuildView


        private void InitChart()
        {
            var items = new List<BarItem>();
            items.Add(new BarItem { Name = "", Value = 0 });
            barChart.Items = items;
        }

        private async void PopulateChart()
        {
            barChart.Items.Clear();

            var items = new List<BarItem>();

            BarGraphHelper b = new BarGraphHelper(viewModel.Orders, false);
            for (int i = b.SalesData.Count - 1; i >= 0; i--)
            {
                WeeklySalesData salesData = b.SalesData.ElementAt(i);
                items.Add(new BarItem() { Name = salesData.DateEndString, Value = Convert.ToInt32(salesData.Amount) });
            } //end foreach

            barChart.Items = items;

            //barChart.Items.Clear();

            //items.Add(new BarItem { Name = "July 20", Value = 10 });
            //items.Add(new BarItem { Name = "July 13", Value = 15 });
            //items.Add(new BarItem { Name = "July 6", Value = 20 });
            //items.Add(new BarItem { Name = "d", Value = 5 });
            //items.Add(new BarItem { Name = "e", Value = 14 });
            //barChart.Items = items;


        }


        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (viewModel.IsInitialized)
            {
                return;
            }
            viewModel.LoadOrdersCommand.Execute(null);
            this.PopulateChart();


            viewModel.IsInitialized = true;
        }


    } //end class
}
