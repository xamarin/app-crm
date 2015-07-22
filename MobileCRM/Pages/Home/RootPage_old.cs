using System;
using MobileCRM.Helpers;
using MobileCRM.Models;
using MobileCRM.Pages.Accounts;
using MobileCRM.Pages.Catalog;
using MobileCRM.Pages.Contacts;
using MobileCRM.Pages.Leads;
using MobileCRM.Pages.Settings;
using MobileCRM.Services;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.ViewModels.Contacts;
using MobileCRM.ViewModels.Leads;
using Xamarin.Forms;
using MenuItem = MobileCRM.Models.MenuItem;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Home
{
    public class RootPage_old : MasterDetailPage
    {
        bool bolSplashShown;

        MenuType previousItem;

        public RootPage_old()
        {
            bolSplashShown = false;

            this.Title = "Verveta CRM";

            this.BackgroundImage = "menubk.png";

            Master = new ContentPage() { Title = "☰" };

            previousItem = MenuType.Leads;//set first time to force
            NavigateTo(MenuType.Dashboard);

            //Authentication notifications
            MessagingCenter.Subscribe<ILogin>(this, MessagingServiceConstants.AUTHENTICATED, (sender) =>
                {
                    this.CloseAuth();

                    var optionsPage = new MenuPage();

                    optionsPage.Menu.ItemSelected += (s, e) =>
                    {

                        var item = e.SelectedItem as MenuItem;
                        if (item == null)
                            return;

                        NavigateTo(item.MenuType);
                        optionsPage.Menu.SelectedItem = null;
                    };

                    Master = optionsPage;
                });    

            //Splash page notification
            MessagingCenter.Subscribe<SplashPage>(this, MessagingServiceConstants.SPLASH_DOWN, (sender) =>
                {
                    bolSplashShown = true;
                    this.CloseSplash();
                });
        }

        void NavigateTo(MenuType option)
        {
            if (previousItem == option)
                return;
           
            previousItem = option;

            var displayPage = PageForOption(option);


            displayPage.BarBackgroundColor = AppColors.CONTENTBKGCOLOR;
            displayPage.BarTextColor = Color.White;
            
      
            Detail = displayPage;

            IsPresented = false;
        }

        NavigationPage dashboard, accounts, leads, contacts, catalog, settings;

        NavigationPage PageForOption(MenuType option)
        {

            switch (option)
            {
                case MenuType.Dashboard:
                    {
                        if (dashboard != null)
                            return dashboard;
                  
                        var vm = new SalesDashboardViewModel(Navigation);

                        dashboard = new NavigationPage(new DashboardView(vm));
                        return dashboard;
                    }
                case MenuType.Accounts:
                    {
                        if (accounts != null)
                            return accounts;

                        var vm = new AccountsViewModel() { Navigation = Navigation };
                        accounts = new NavigationPage(new AccountsView(vm));

                        return accounts; 
                    }
                case MenuType.Leads:
                    {
                        if (leads != null)
                            return leads;
                
                        leads = new NavigationPage(new LeadsView(new LeadsViewModel() { Navigation = Navigation }));
                        return leads;
                    }
                case MenuType.Contacts:
                    {
                        if (contacts != null)
                            return contacts;
                        var vm = new ContactsViewModel();
                        contacts = new NavigationPage(new ContactsView(vm));
                        return contacts;
                    } 
                case MenuType.Catalog:
                    {
                        if (catalog != null)
                            return catalog;

                        catalog = new NavigationPage(new CatalogCarouselView());
                        return catalog;
                    }

                case MenuType.Settings:
                    {
                        if (settings != null)
                            return settings;

                        settings = new NavigationPage(new UserSettingsView());
                        return settings;
                    }
            }
            
            throw new NotImplementedException("Unknown menu option: " + option.ToString());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Show splash page only iOS & Android. 
            //WP implementation has some quirks.

            if ((Device.OS == TargetPlatform.iOS) || (Device.OS == TargetPlatform.Android))
            {
                if (!bolSplashShown)
                {
                    Navigation.PushModalAsync(new SplashPage());
                }
            } //end if
        }

        void CloseAuth()
        {
            Navigation.PopModalAsync();
        }

        //Close splash page, show login page.
        //For demo purposes we remove cached login and prompt for credentials every time.
        void CloseSplash()
        {
            Navigation.PopModalAsync();

            AuthInfo.Instance.GetMobileServiceClient().Logout();
            AuthInfo.Instance.User = null;

            Navigation.PushModalAsync(new LoginPage());

        }
    }
}