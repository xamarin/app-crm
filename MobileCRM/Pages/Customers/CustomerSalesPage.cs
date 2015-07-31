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

            #region sales chart header
            #endregion

            #region sales chart
            #endregion

            Content = StackLayout;
        }
    }
}

