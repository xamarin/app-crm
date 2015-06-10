using MobileCRM.Models;
using MobileCRM.Services;
using MobileCRM.Pages.Accounts;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.ViewModels.Contacts;
using MobileCRM.ViewModels.Home;
using System;
using Xamarin.Forms;

namespace MobileCRM.Pages.Home
{
    public class RootPage : MasterDetailPage
    {
        bool bolSplashShown;

        MenuType previousItem;

        public RootPage()
        {
            bolSplashShown = false;

            this.Title = "Verveta CRM";

            this.BackgroundImage = "menubk.png";

            Master = new ContentPage() { Title = "MENU" };

            previousItem = MenuType.Leads;//set first time to force
            NavigateTo(MenuType.Dashboard);

            //Authentication notifications
            MessagingCenter.Subscribe<ILogin>(this, "Authenticated", (sender) =>
                {
                    this.CloseAuth();

                    var optionsPage = new MenuPage();

                    optionsPage.Menu.ItemSelected += (s, e) =>
                    {

                        var item = e.SelectedItem as MobileCRM.Models.MenuItem;
                        if (item == null)
                            return;

                        NavigateTo(item.MenuType);
                        optionsPage.Menu.SelectedItem = null;
                    };

                    Master = optionsPage;
                });    

            //Splash page notification
            MessagingCenter.Subscribe<SplashPage>(this, "SplashShown", (sender) =>
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


            displayPage.BarBackgroundColor = Helpers.AppColors.CONTENTBKGCOLOR;
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
                  
                        var vm = new DashboardViewModel() { Navigation = Navigation };

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
                
                        leads = new NavigationPage(new Leads.LeadsView(new ViewModels.Leads.LeadsViewModel() { Navigation = Navigation }));
                        return leads;
                    }
                case MenuType.Contacts:
                    {
                        if (contacts != null)
                            return contacts;
                        var vm = new ContactsViewModel();
                        contacts = new NavigationPage(new Contacts.ContactsView(vm));
                        return contacts;
                    } 
                case MenuType.Catalog:
                    {
                        if (catalog != null)
                            return catalog;

                        catalog = new NavigationPage(new Catalog.CatalogCarouselView());
                        return catalog;
                    }

                case MenuType.Settings:
                    {
                        if (settings != null)
                            return settings;

                        settings = new NavigationPage(new Settings.UserSettingsView());
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