using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MobileCRM.Shared.Interfaces;


namespace MobileCRM.Shared.Pages.Settings
{
    public partial class DataSettingsView
    {
        IDataManager dataManager;

        public DataSettingsView()
        {
            InitializeComponent();

            dataManager = DependencyService.Get<IDataManager>();

            bool bolInit = dataManager.DoesLocalDBExist();

            lblDataInit.Text = bolInit.ToString();
        }
    }
}
