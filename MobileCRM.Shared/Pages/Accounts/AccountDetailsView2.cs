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
        AccountDetailsViewModel viewModelAcct;
        Account acct;
        OrdersViewModel viewModelOrders;


        CustomControls.BarChart barChart;
        TableSection sectionSales;
        TextCell txtNoOrders;

        public AccountDetailsView2(AccountDetailsViewModel vmAcct, OrdersViewModel vmOrders)
        {
            Console.WriteLine("AccountDetailsView2 Constructor called...");

            this.BindingContext = viewModelOrders = vmOrders;
            viewModelAcct = vmAcct;
            acct = viewModelAcct.Account;

            //SetBinding(Page.TitleProperty, new Binding("Title"));
            //SetBinding(Page.IconProperty, new Binding("Icon"));
            this.Title = viewModelAcct.Title;
            this.Icon = viewModelAcct.Icon;


            this.Content = this.BuildView();
        }


        private Layout BuildView()
        {
            ScrollView scroll = new ScrollView();
            //ContentView scroll = new ContentView();

            try
            {
                TableSection sectionInfo = new TableSection("COMPANY INFO");

                TextCell cellCompany = new TextCell() { Text = "Company", Detail = acct.Company };
                sectionInfo.Add(cellCompany);

                TextCell cellIndustry = new TextCell() { Text = "Industry", Detail = acct.Industry };
                sectionInfo.Add(cellIndustry);

                TextCell cellContact = new TextCell() { Text = "Contact", Detail = viewModelAcct.DisplayContact };
                sectionInfo.Add(cellContact);

                TextCell cellPhone = new TextCell() { Text = "Phone", Detail = acct.Phone };
                sectionInfo.Add(cellPhone);

                TextCell cellAddr = new TextCell() { Text = "Address", Detail = acct.AddressString };
                sectionInfo.Add(cellAddr);


                sectionSales = new TableSection("SALES");


                barChart = new CustomControls.BarChart() { HeightRequest = 200 };

                //this.InitChart();

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

                scroll = new ScrollView() { Content = stack };
                //scroll = new ContentView() { Content = stack };

            }
            catch (Exception exc)
            {
                Console.WriteLine("EXCEPTION: AccountDetailsView2.BuildView(): " + exc.Message + "  |  " + exc.StackTrace);
            }
            finally
            {
               
            }

            return scroll;
        } //end BuildView


        private void InitChart()
        {
            var items = new List<BarItem>();
            items.Add(new BarItem { Name = "No Orders", Value = 1 });
            barChart.Items = items;
        }

        private async void PopulateChart()
        {
            try {

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

                    barChart.Items = orderedData;
                } //end if

            } catch (Exception exc) {
                Console.WriteLine("EXCEPTION: AccountDetailsView2.PopulateChart(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        }


        protected async override void OnAppearing()
        //protected override void OnAppearing()
        {
            try
            {

                base.OnAppearing();

                //this.PopulateChart();

                if (viewModelOrders.IsInitialized)
                {
                    return;
                }
                
                await viewModelOrders.ExecuteLoadOrdersCommand();

                this.PopulateChart();

                viewModelOrders.IsInitialized = true;

            }
            catch (Exception exc)
            {
                Console.WriteLine("EXCEPTION: AccountDetailsView2.OnAppearing(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        }


        public void RefreshView()
        {
            //viewModelOrders.LoadOrdersCommand.Execute(null);
            //viewModelOrders.IsInitialized = true;
            this.PopulateChart();
        }

    } //end class
}
