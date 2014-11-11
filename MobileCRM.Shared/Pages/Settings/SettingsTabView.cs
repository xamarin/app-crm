using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileCRM.Shared.Pages.Settings
{
    public class SettingsTabView : TabbedPage
    {

        public SettingsTabView()
        {
            this.Title = "Settings";


            this.Children.Add(new UserSettingsView() { Title = "User Info" });


            this.Children.Add(new DataSettingsView() { Title = "Data Settings" });

        } //end ctor

    }
}
