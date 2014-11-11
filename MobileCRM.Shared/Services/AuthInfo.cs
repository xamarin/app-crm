using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Shared.Models;


namespace MobileCRM.Shared.Services
{
    public class AuthInfo
    {
        public static string APPLICATION_URL = "https://xamarin3crmdemoprod.azure-mobile.net/";
        public static string APPLICATION_KEY = "eeEXCKVPqNtEOIhypkxzgAMUUdEjhN69";

        private static AuthInfo instance;

        private MobileServiceUser user;
        private MobileServiceClient client;
        private UserInfo userInfo;

        public const MobileServiceAuthenticationProvider AUTH_PROVIDER = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;


        private AuthInfo()
        {
            user = null;
            userInfo = null;
            client = new MobileServiceClient(
                        AuthInfo.APPLICATION_URL,
                        AuthInfo.APPLICATION_KEY);
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
                UserInfo = await client.InvokeApiAsync<UserInfo>("getidentities", System.Net.Http.HttpMethod.Get, null);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("ERROR AuthInfo.GetUserInf(): " + exc.Message);
            }
        }


    }
}
