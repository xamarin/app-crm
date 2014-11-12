using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MobileCRM.Shared.Services;


namespace MobileCRM.Shared.Pages.Home
{
    public partial class SplashPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }


        void OnLoginClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send<SplashPage>(this, "SplashShown");

        }


    }
}
