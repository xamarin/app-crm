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
        IConfigFetcher _ConfigFetcher;

        static AuthInfo instance;

        MobileServiceUser user;
        MobileServiceClient client;
        UserInfo userInfo;

        public const MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;

        AuthInfo()
        {
            _ConfigFetcher = DependencyService.Get<IConfigFetcher>();

            user = null;
            userInfo = null;
            client = new MobileServiceClient(
                _ConfigFetcher.GetAsync("azureMobileServiceJsUrl").Result,
                _ConfigFetcher.GetAsync("azureMobileServiceJsAppKey", true).Result);
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
            get { return user; }
            set
            {
                user = value;

                //Insights user tracking
                if (value != null)
                {
                    Insights.Identify(user.UserId, "email", "sally@xamcrm.onmicrosoft.com");
                } //end if

            }
        }

        public UserInfo UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; }
        }

        public MobileServiceClient GetMobileServiceClient()
        {
            return client;
        }

        public async Task GetUserInfo()
        {
            try
            {
                UserInfo = await client.InvokeApiAsync<UserInfo>("getidentities", HttpMethod.Get, null);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("ERROR AuthInfo.GetUserInf(): " + exc.Message);
            }
        }
    }
}