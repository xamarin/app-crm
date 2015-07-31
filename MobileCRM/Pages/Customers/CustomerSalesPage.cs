using System;
using Xamarin.Forms;
using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.ViewModels.Accounts;

namespace MobileCRM
{
    public class CustomerSalesPage : BaseCustomerDetailPage
    {
        public CustomerSalesPage()
        {
            BackgroundColor = Color.Green;

            Content = StackLayout;
        }
    }
}

