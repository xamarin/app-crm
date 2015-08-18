using Xamarin.Forms;
using XamarinCRM.Pages.Customers;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerTabbedPage : TabbedPage
    {
        public CustomerTabbedPage(INavigation navigation, Account account)
        {
            // since we're modally presented this tabbed view (because Android natviely doesn't support nested tabs),
            // this tool bar item provides a way to get back to the Customers list
            ToolbarItems.Add(new ToolbarItem(TextResources.Customers_Orders_CustomerTabbedPage_BackToCustomers, null, () => navigation.PopModalAsync()));

            CustomerDetailPage customerDetailPage = new CustomerDetailPage(new CustomerDetailViewModel(account) { Navigation = this.Navigation })
            {
                Title = TextResources.Customers_Detail_Tab_Title,
                Icon = new FileImageSource() { File = "CustomersTab" } // only used  on iOS
            };

            CustomerOrdersPage customerOrdersPage = new CustomerOrdersPage(new OrdersViewModel(account) { Navigation = this.Navigation })
            {
                Title = TextResources.Customers_Orders_Tab_Title,
                Icon = new FileImageSource() { File = "ProductsTab" } // only used  on iOS
            };

            CustomerSalesPage customerSalesPage = new CustomerSalesPage(new CustomerSalesViewModel(account) { Navigation = this.Navigation })
            {
                Title = TextResources.Customers_Sales_Tab_Title,
                Icon = new FileImageSource() { File = "SalesTab" } // only used  on iOS
            };

            Children.Add(customerDetailPage);
            Children.Add(customerOrdersPage);
            Children.Add(customerSalesPage);

        }
    }
}

