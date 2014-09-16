using MobileCRM.Shared.Models;
using MobileCRM.Shared.Pages.Accounts;
using MobileCRM.Shared.Pages.Catalog;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Home;
using MobileCRM.Shared.ViewModels.Catalog;

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Home
{
    public class RootView : MasterDetailPage
    {
        MenuType previousItem;
        public RootView ()
        {
          this.Title = "Mobile CRM";
            var optionsPage = new MenuView ();

            optionsPage.Menu.ItemSelected += (sender, e) =>
            {

              var item = e.SelectedItem as MenuItem;
              if (item == null)
                return;

              NavigateTo(item.MenuType);
              optionsPage.Menu.SelectedItem = null;
            };

            Master = optionsPage;

            previousItem = MenuType.Leads;//set first time to force
            NavigateTo(MenuType.Dashboard);

            //ShowLoginDialog();    
        }

        async void ShowLoginDialog()
        {
            var page = new LoginPage();

            await Navigation.PushModalAsync(page);
        }

        void NavigateTo(MenuType option)
        {
            if (previousItem == option)
              return;
           
            previousItem = option;

            var displayPage = PageForOption(option);

            displayPage.BarBackgroundColor = Helpers.Color.Blue.ToFormsColor();
            displayPage.BarTextColor = Color.White;
            
             
#if WINDOWS_PHONE
            Detail = new ContentPage();
#endif

            Detail = displayPage;

            IsPresented = false;
        }

        NavigationPage dashboard, accounts, leads, contacts, catalog;
        NavigationPage PageForOption (MenuType option)
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
                
                leads =  new NavigationPage(new Leads.LeadsView(new ViewModels.Leads.LeadsViewModel() { Navigation = Navigation }));
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

                  //var vm = new CatalogViewModel() { Navigation = Navigation };
                  catalog = new NavigationPage(new Catalog.CatalogCarouselView());
                  return catalog;
              }
          }
            
          throw new NotImplementedException("Unknown menu option: " + option.ToString());
        }
    }
}
