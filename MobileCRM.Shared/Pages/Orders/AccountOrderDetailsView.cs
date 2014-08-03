using System;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using MobileCRM.Shared.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Accounts
{
    public class AccountOrderDetailsView : BaseView
    {
        private OrderDetailsViewModel viewModel;

        private CustomControls.SignaturePad sigPad;

        public AccountOrderDetailsView(Order order)
        {
            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.Title = "Complete Order";

            this.BindingContext = this.viewModel = new OrderDetailsViewModel(Navigation, order);

            this.Content = this.BuildView();
        } //end ctor

        private Layout BuildView()
        {
            Label lblInv = new Label() { Text = "ORDER INVOICE", HorizontalOptions = LayoutOptions.Center, TextColor = Color.Blue };

            Label lblProd = new Label() { Text = "Product:", TextColor = Color.Blue };
            Label lblProdData = new Label();
            lblProdData.SetBinding(Label.TextProperty, "Order.Item");

            Label lblPrice = new Label() { Text = "Original Price Quote:", TextColor = Color.Blue };
            Label lblPriceData = new Label() { Text = "$" + viewModel.Price };


            Label lblDateEntered = new Label() { Text = "Date Entered:", TextColor = Color.Blue };
            Label lblDateEnteredData = new Label() { Text = viewModel.Order.OrderDate.ToString("d") };


            Label lblDateDue = new Label() { Text = "Date Due:", TextColor = Color.Blue };
            Label lblDateDueData = new Label() { Text = viewModel.Order.DueDate.ToString("d") };

            
            Label lblFinalPrice = new Label() { Text = "Final Price:", TextColor = Color.Blue };
            Entry entryPrice = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric };
            entryPrice.SetBinding(Entry.TextProperty, "Price");

            //Closed Order
            Label lblFinalPriceData = new Label() { Text = viewModel.Price };


            Label lblDateClosed = new Label() { Text = "Date Completed:", TextColor = Color.Blue };
            DatePicker dateClosed = new DatePicker();
            dateClosed.SetBinding(DatePicker.DateProperty, "Order.ClosedDate");

            //Closed order
            Label lblDateClosedData = new Label() { Text = viewModel.Order.ClosedDate.ToString("d") };


            Label lblSig = new Label() { Text = "Signature:", TextColor = Color.Blue };
            sigPad = new CustomControls.SignaturePad() { HeightRequest = 100, HorizontalOptions = LayoutOptions.FillAndExpand };
            sigPad.SetBinding(CustomControls.SignaturePad.DefaultPointsProperty, "Order.Signature");
            
            //Display but disable signature pad on completed orders.
            sigPad.SetBinding(CustomControls.SignaturePad.IsEnabledProperty, "Order.IsOpen");


            Button btnComplete = new Button() { Text = "Order Complete", HorizontalOptions = LayoutOptions.Center };
            btnComplete.Clicked += btnComplete_Clicked;

            Button btnCancel = new Button() { Text = "Go Back", HorizontalOptions = LayoutOptions.Center };
            btnCancel.Clicked += btnCancel_Clicked;

            StackLayout stack;

            //Present read-only view if order is closed and viewing it from History
            if (viewModel.Order.IsOpen)
            {
                stack = new StackLayout()
                {
                    Padding = new Thickness(10, 20, 10, 5),
                    Children =  
                    {
                        lblInv,
                    
                        lblProd,
                        lblProdData,

                        lblPrice,
                        lblPriceData,

                        lblDateDue,
                        lblDateDueData,

                        lblFinalPrice,
                        entryPrice,

                        lblDateClosed,
                        dateClosed,

                        lblSig,
                        sigPad,

                        btnComplete,
                        btnCancel
                    }
                };            
            }
            else
            {
                stack = new StackLayout()
                {
                    Padding = 10,
                    Children =  
                    {
                        lblInv,
                    
                        lblProd,
                        lblProdData,

                        lblPrice,
                        lblPriceData,

                        lblDateDue,
                        lblDateDueData,

                        lblFinalPrice,
                        //entryPrice,
                        lblFinalPriceData,

                        lblDateClosed,
                        //dateClosed,
                        lblDateClosedData,

                        lblSig,
                        sigPad

                        //btnComplete              
                    }
                }; 
            } //end else

            

            return stack;
        }

        void btnCancel_Clicked(object sender, System.EventArgs e)
        {
            viewModel.GoBack();
        }


        public void btnComplete_Clicked(object sender, EventArgs e)
        {
            var signature = string.Empty;
            if (sigPad.GetPointString != null)
                signature = sigPad.GetPointString.Invoke();

            if (string.IsNullOrWhiteSpace(signature) || signature == "[]")
            {
                DisplayAlert("Error", "Signature required", "OK", null);
                return;
            }
            viewModel.Order.Signature = signature;

            viewModel.IsInitialized = false;

            viewModel.ApproveOrderCommand.Execute(null);
        }

    } //end class
}
