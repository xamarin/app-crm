using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using MobileCRM.Shared.Services;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels;



namespace MobileCRM.Shared.ViewModels.Settings
{
    public class UserViewModel : BaseViewModel
    {

        private UserInfo userInfo;
        private string displayAddress;


        public UserViewModel()
        {
            userInfo = AuthInfo.Instance.UserInfo;
            //userInfo = null;
        }

        //public UserViewModel(UserInfo userInfo)
        //{
        //    this.userInfo = userInfo;

        //} //end ctor


        public UserInfo UserInfo
        {
            get { return userInfo; }
            set
            {
                userInfo = value;

                this.SetLocationInfo();

                OnPropertyChanged("UserInfo");
            }
        }


        private async Task SetLocationInfo()
        {
            this.DisplayAddress =
                userInfo.StreetAddress + Environment.NewLine +
                userInfo.City + ", " +
                userInfo.State + " " +
                userInfo.PostalCode + ", " +
                userInfo.Country;
        }

        public string DisplayAddress
        {
            get
            {
                return displayAddress;
            }
            set
            {
                displayAddress = value;
                OnPropertyChanged("DisplayAddress");
            }
        }

    }
}
