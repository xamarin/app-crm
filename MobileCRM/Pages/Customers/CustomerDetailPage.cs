using System;
using Xamarin.Forms;
using MobileCRM.Models;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.Pages.Base;

namespace MobileCRM
{
    public class CustomerDetailPage : BaseCustomerDetailPage
    {
        public CustomerDetailPage()
        {
            BackgroundColor = Color.Red;

            Content = StackLayout;
        }
    }
}

