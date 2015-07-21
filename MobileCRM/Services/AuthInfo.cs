using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Models;
using Xamarin;

namespace MobileCRM.Services
{
    public class AuthInfo
    {
        public static string APPLICATION_URL = "https://xamarin3crmdemoprod.azure-mobile.net/";
        public static string APPLICATION_KEY = "eeEXCKVPqNtEOIhypkxzgAMUUdEjhN69";

        static AuthInfo instance;

        MobileServiceUser user;
        MobileServiceClient client;
        UserInfo userInfo;

        public const MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;

        AuthInfo()
        {
            user = null;
            userInfo = null;
            client = new MobileServiceClient(
                APPLICATION_URL,
                APPLICATION_KEY);
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