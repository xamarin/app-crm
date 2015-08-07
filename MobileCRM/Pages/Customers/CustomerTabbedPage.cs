using System;
using Xamarin.Forms;
using MobileCRM.Pages.Customers;
using MobileCRM.ViewModels.Customers;
using MobileCRM.Models;
using MobileCRM.Customers;

namespace MobileCRM
{
    public class CustomerTabbedPage : TabbedPage
    {
        public CustomerTabbedPage(INavigation navigation, Account account)
        {
            // since we're modally presented this tabbed view (because Android natviely doesn't support nested tabs),
            // this tool bar item provides a way to get back to the Customers list
            ToolbarItems.Add(new ToolbarItem("Back to Customers", null, () => navigation.PopModalAsync()));

            Children.Add(new CustomerDetailPage()
                {
                    Title = TextResources.Customers_Detail_Tab_Title,
                    BindingContext = new CustomerDetailViewModel(account) { Navigation = navigation },
                    Icon = new FileImageSource() { File = "CustomersTab" },
                });

            Children.Add(new CustomerOrdersPage()
                {
                    Title = TextResources.Customers_Orders_Tab_Title,
                    BindingContext = new OrdersViewModel(account) { Navigation = navigation },
                    Icon = new FileImageSource() { File = "ProductsTab" }
                });

            Children.Add(new CustomerSalesPage()
                {
                    Title = TextResources.Customers_Sales_Tab_Title,
                    BindingContext = new CustomerSalesViewModel() { Navigation = navigation },
                    Icon = new FileImageSource() { File = "SalesTab" }
                });

        }
    }
}

