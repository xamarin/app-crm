using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Models;
using Xamarin;
using Xamarin.Forms;

namespace MobileCRM.Services
{
    public class AuthInfo
    {
        readonly IConfigFetcher _ConfigFetcher;
        readonly MobileServiceClient _Client;
        MobileServiceUser _User;
        UserInfo _UserInfo;

        static AuthInfo instance;

        public const MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;

        AuthInfo()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            _User = null;
            _UserInfo = null;
            _Client = new MobileServiceClient(
                _ConfigFetcher.GetAsync("customerDataServiceUrl").Result,
                _ConfigFetcher.GetAsync("customerDataServiceAppKey", true).Result);
        }

        public static AuthInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthInfo();
                }
                return instance;
            }
        }

        public MobileServiceUser User
        {
            get { return _User; }
            set
            {
                _User = value;

                //Insights user tracking
                if (value != null)
                {
                    Insights.Identify(_User.UserId, "email", "sally@xamcrm.onmicrosoft.com");
                } //end if

            }
        }

        public UserInfo UserInfo
        {
            get { return _UserInfo; }
            set { _UserInfo = value; }
        }

        public MobileServiceClient GetMobileServiceClient()
        {
            return _Client;
        }

        public async Task GetUserInfo()
        {
            try
            {
                UserInfo = await _Client.InvokeApiAsync<UserInfo>("getidentities", HttpMethod.Get, null);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("ERROR AuthInfo.GetUserInf(): " + exc.Message);
            }
        }
    }
}