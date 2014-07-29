using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
    public class NewOrderView2 : BaseView
    {
        OrderDetailsViewModel viewModel;

        public NewOrderView2(string accountId)
        {
            this.BindingContext = viewModel = new OrderDetailsViewModel(Navigation, new Order() { AccountId = accountId });

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.Title = "New Order";

            this.Content = this.BuildView();

        } //end ctor


        private Layout BuildView()
        {
            Label lblItem = new Label() { Text = "Product:" };
            Picker pickerItem = new Picker() { HorizontalOptions = LayoutOptions.FillAndExpand };

            foreach(var a in Order.ItemTypes)
            {
                pickerItem.Items.Add(a);
            }
            pickerItem.SetBinding(Picker.SelectedIndexProperty, "ItemIndex");

            Label lblPrice = new Label() { Text = "Price:" };
            Entry entryPrice = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand, Keyboard = Keyboard.Numeric };
            entryPrice.SetBinding(Entry.TextProperty, "Price");

            Label lblDateDue = new Label() { Text = "Date Due:" };
            DatePicker dateDue = new DatePicker() { HorizontalOptions = LayoutOptions.FillAndExpand };
            dateDue.SetBinding(DatePicker.DateProperty, "Order.DueDate");

            Button btnOrder = new Button() { Text = "Place Order", HorizontalOptions = LayoutOptions.Center };
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

        private void btnOrder_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveOrderCommand.Execute(null);
        }



    }
}
