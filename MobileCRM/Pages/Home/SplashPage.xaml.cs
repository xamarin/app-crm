using System;
using Xamarin.Forms;

namespace MobileCRM.Pages.Home
{
    public partial class SplashPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        void OnLoginClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send<SplashPage>(this, MessagingServiceConstants.SPLASH_DOWN);
        }
    }
}