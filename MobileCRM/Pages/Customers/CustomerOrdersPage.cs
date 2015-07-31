using System;
using Xamarin.Forms;
using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.ViewModels.Accounts;

namespace MobileCRM
{
    public class CustomerOrdersPage : BaseCustomerDetailPage
    {
        public CustomerOrdersPage()
        {
            BackgroundColor = Color.Blue;

            Content = StackLayout;
        }
    }
}

