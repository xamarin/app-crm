using MobileCRM.Shared.ViewModels.Settings;

namespace MobileCRM.Shared.Pages.Settings
{
    public partial class UserSettingsView
    {
        private UserViewModel vm;

        public UserSettingsView()
        {
            InitializeComponent();

            this.Title = "About VervetaCRM";

            //SYI: v2 feature.
            //this.BindingContext = vm = new UserViewModel(); 
        }
    }
}