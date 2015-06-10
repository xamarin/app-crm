using MobileCRM.Models;
using MobileCRM.ViewModels.Orders;
using MobileCRM.Helpers;
using System;
using Xamarin.Forms;
using Xamarin;

namespace MobileCRM.Pages.Orders
{
    public class NewOrderView : BaseView
    {
        OrderDetailsViewModel viewModel;

        public NewOrderView(string accountId)
        {
            this.BindingContext = viewModel = new OrderDetailsViewModel(Navigation, new Order() { AccountId = accountId });

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.Title = "New Order";

            this.Content = this.BuildView();

            Insights.Track("New Order Page");
        }

        Layout BuildView()
        {
            this.BackgroundColor = AppColors.CONTENTLIGHTBKG;

            Label lblItem = new Label() { Text = "Product:",  TextColor = AppColors.LABELBLUE };
            Picker pickerItem = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = AppColors.LABELGRAY };

            foreach (var a in Order.ItemTypes)
            {
                pickerItem.Items.Add(a);
            }
            pickerItem.SetBinding(Picker.SelectedIndexProperty, "ItemIndex");

            Label lblPrice = new Label() { Text = "Price:", TextColor = AppColors.LABELBLUE };
            Entry entryPrice = new Entry()
            { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric,
                BackgroundColor = AppColors.LABELGRAY
            };
            entryPrice.SetBinding(Entry.TextProperty, "Price");

            Label lblDateDue = new Label() { Text = "Date Due:", TextColor = AppColors.LABELBLUE };
            DatePicker dateDue = new DatePicker() { HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = AppColors.LABELGRAY };
            dateDue.SetBinding(DatePicker.DateProperty, "Order.DueDate");

            Button btnOrder = new Button() { Text = "Place Order", HorizontalOptions = LayoutOptions.Center, TextColor = AppColors.LABELWHITE };
            btnOrder.Clicked += btnOrder_Clicked;

            StackLayout stack = new StackLayout()
            {
                Padding = 10,

                Children =
                {
                    lblItem,
                    pickerItem,
                    lblPrice,
                    entryPrice,
                    lblDateDue,
                    dateDue,
                    btnOrder
                }
            };

            return stack;
        }

        void btnOrder_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveOrderCommand.Execute(null);
        }
    }
}