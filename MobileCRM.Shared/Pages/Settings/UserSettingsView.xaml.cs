using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MobileCRM.Shared.Services;
using MobileCRM.Shared.ViewModels.Settings;


namespace MobileCRM.Shared.Pages.Settings
{
    public partial class UserSettingsView
    {
        private UserViewModel vm;

        public UserSettingsView()
        {
            InitializeComponent();

            this.Title = "About Mobile CRM";

            //SYI: v2 feature.
            //this.BindingContext = vm = new UserViewModel(); 

        }
    }
}
