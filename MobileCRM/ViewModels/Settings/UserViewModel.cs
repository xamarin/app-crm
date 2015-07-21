using System;
using System.Threading.Tasks;
using MobileCRM.Services;
using MobileCRM;
using MobileCRM.Models;

namespace MobileCRM.ViewModels.Settings
{
    public class UserViewModel : BaseViewModel
    {
        UserInfo userInfo;
        string displayAddress;

        public UserViewModel()
        {
            userInfo = AuthInfo.Instance.UserInfo;
        }

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

        async Task SetLocationInfo()
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