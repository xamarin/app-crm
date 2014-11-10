using System;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using MobileCRM.Shared.Helpers;
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
            Label lblInv = new Label() { Text = "ORDER INVOICE", 
                HorizontalOptions = LayoutOptions.FillAndExpand, 
                XAlign = TextAlignment.Center,
                TextColor = AppColors.LABELWHITE,
                BackgroundColor = AppColors.CONTENTBKGCOLOR,
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
            };
            StackLayout stackHeaderInv = new StackLayout()
            {
                Padding = new Thickness(0, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = AppColors.CONTENTBKGCOLOR,
            };
            stackHeaderInv.Children.Add(lblInv);

            Label lblProd = new Label() { Text = "Product:", TextColor = AppColors.LABELBLUE, 
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold) };
            Label lblProdData = new Label() { TextColor = AppColors.LABELWHITE };
            lblProdData.SetBinding(Label.TextProperty, "Order.Item");

            Label lblPrice = new Label() { Text = "Original Price Quote:", TextColor = AppColors.LABELBLUE,
                 Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold) };
            Label lblPriceData = new Label() { Text = "$" + viewModel.Price, TextColor = AppColors.LABELWHITE };


            Label lblDateEntered = new Label() { Text = "Date Entered:", TextColor = AppColors.LABELBLUE,
                 Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold) };
            Label lblDateEnteredData = new Label() { Text = viewModel.Order.OrderDate.ToString("d"), TextColor = AppColors.LABELWHITE };


            Label lblDateDue = new Label()
            {
                Text = "Date Due:",
                TextColor = AppColors.LABELBLUE,
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold)
            };
            Label lblDateDueData = new Label() { Text = viewModel.Order.DueDate.ToString("d"), TextColor = AppColors.LABELWHITE };


            Label lblFinalPrice = new Label()
            {
                Text = "Final Price:",
                TextColor = AppColors.LABELBLUE,
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold)
            };
            Entry entryPrice = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric,
                BackgroundColor = AppColors.LABELGRAY};
            entryPrice.SetBinding(Entry.TextProperty, "Price");

            //Closed Order
            Label lblFinalPriceData = new Label() { Text = viewModel.Price, TextColor = AppColors.LABELWHITE };


            Label lblDateClosed = new Label()
            {
                Text = "Date Completed:",
                TextColor = AppColors.LABELBLUE,
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold)
            };
            DatePicker dateClosed = new DatePicker();
            dateClosed.BackgroundColor = AppColors.LABELGRAY;
            dateClosed.SetBinding(DatePicker.DateProperty, "Order.ClosedDate");

            //Closed order
            Label lblDateClosedData = new Label() { Text = viewModel.Order.ClosedDate.ToString("d"), TextColor = AppColors.LABELWHITE };


            Label lblSig = new Label() { Text = "SIGNATURE",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                XAlign = TextAlignment.Center,
                TextColor = AppColors.LABELWHITE,
                BackgroundColor = AppColors.CONTENTBKGCOLOR,
                Font = Font.SystemFontOfSize(NamedSize.Medium, FontAttributes.Bold),
            };
            StackLayout stackHeaderSig = new StackLayout()
            {
                Padding = new Thickness(0, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = AppColors.CONTENTBKGCOLOR,
            };
            stackHeaderSig.Children.Add(lblSig);


            sigPad = new CustomControls.SignaturePad() { HeightRequest = 100, HorizontalOptions = LayoutOptions.FillAndExpand };
            sigPad.SetBinding(CustomControls.SignaturePad.DefaultPointsProperty, "Order.Signature");
            
            //Display but disable signature pad on completed orders.
            sigPad.SetBinding(CustomControls.SignaturePad.IsEnabledProperty, "Order.IsOpen");


            Button btnComplete = new Button() { Text = "Order Complete", HorizontalOptions = LayoutOptions.Center, TextColor = AppColors.LABELWHITE };
            btnComplete.Clicked += btnComplete_Clicked;

            Button btnCancel = new Button() { Text = "Go Back", HorizontalOptions = LayoutOptions.Center, TextColor = AppColors.LABELWHITE };
            btnCancel.Clicked += btnCancel_Clicked;

            StackLayout stack;

            //Present read-only view if order is closed and viewing it from History
            if (viewModel.Order.IsOpen)
            {
                stack = new StackLayout()
                {
                    Padding = 0,
                    Spacing = 0,
                    Children =  
                    {
                        stackHeaderInv,
                    
                        new StackLayout()
                        {
                            Spacing = 5,
                            Padding = 10,
                            Children = 
                            {
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

                            }
                        },

                       

                        stackHeaderSig,
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
                    Padding = 0,
                    Spacing = 0,
                    Children =  
                    {
                        stackHeaderInv,

                        new StackLayout()
                        {
                            Spacing = 5,
                            Padding = 10,
                            Children = 
                            {
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
                            }

                        },
                    
                       

                        stackHeaderSig,
                        sigPad

                        //btnComplete              
                    }
                }; 
            } //end else


            stack.BackgroundColor = AppColors.CONTENTLIGHTBKG;

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
