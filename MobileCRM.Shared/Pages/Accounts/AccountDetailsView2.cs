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
        TableSection sectionSales;
        TextCell txtNoOrders;

        public AccountDetailsView2(AccountDetailsViewModel vm)
        {
            Console.WriteLine("AccountDetailsView2 Constructor called...");


            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;

            //ordersviewModel = new OrdersViewModel(false, vm.Account.Id);


            this.Content = this.BuildView();
        }


        private Layout BuildView()
        {
            ScrollView scroll = new ScrollView();
            //ContentView scroll = new ContentView();

            try
            {
                TableSection sectionInfo = new TableSection("COMPANY INFO");

                TextCell cellCompany = new TextCell() { Text = "Company" };
                cellCompany.SetBinding(TextCell.DetailProperty, "Account.Company");
                sectionInfo.Add(cellCompany);

                TextCell cellIndustry = new TextCell() { Text = "Industry" };
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


                sectionSales = new TableSection("SALES");

                //TODO - Renders in iOS but not Android
                //txtNoOrders = new TextCell() { Text = " ", Detail = " " };
                //sectionSales.Add(txtNoOrders);


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

                if (viewModel.Orders.Count() > 0)
                {
                    var barData = new BarGraphHelper(viewModel.Orders, false);


                    var orderedData = (from data in barData.SalesData
                                       orderby data.DateStart
                                       select new BarItem
                                       {
                                           Name = data.DateStartString,
                                           Value = Convert.ToInt32(data.Amount)
                                       }).ToList();

                    barChart.Items = orderedData;
                }
                else
                {
                    //TODO - Renders in iOS but not Android.

                    //txtNoOrders.Text = "No Orders";
                    //txtNoOrders.Detail = "No Orders";
                    //txtNoOrders.DetailColor = Xamarin.Forms.Color.Gray;
                }
            } catch (Exception exc) {
                Console.WriteLine("EXCEPTION: AccountDetailsView2.PopulateChart(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        }


        protected async override void OnAppearing()
        {
            try
            {

                base.OnAppearing();


                if (viewModel.IsInitialized)
                {
                    return;
                }
                
                await viewModel.ExecuteLoadOrdersCommand();
                this.PopulateChart();


                viewModel.IsInitialized = true;

            }
            catch (Exception exc)
            {
                Console.WriteLine("EXCEPTION: AccountDetailsView2.OnAppearing(): " + exc.Message + "  |  " + exc.StackTrace);
            }

        }


    } //end class
}
