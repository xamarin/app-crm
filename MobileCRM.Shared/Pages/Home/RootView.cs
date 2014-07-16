using MobileCRM.Shared.Models;
using MobileCRM.Shared.Pages.Accounts;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
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

#if WINDOWS_PHONE
            Detail = new ContentPage();//work around to clear current page.
#endif
            Detail = new NavigationPage(displayPage)
            {
              Tint = Helpers.Color.Blue.ToFormsColor(),
            };

            IsPresented = false;
        }

        Page PageForOption (MenuType option)
        {

          switch (option)
          {
            case MenuType.Dashboard:
              {
                return new DashboardView();
              }
            case MenuType.Accounts:
              {
                var vm = new AccountsViewModel();
                return new TabView("Accounts", new Page[]
                  {
                    new AccountsView(vm),
                    new AccountsMapView(vm)
                  }, vm);      
              }
            case MenuType.Leads:
              {
                return new Leads.LeadsView(new ViewModels.Leads.LeadsViewModel()); ;
              }
            case MenuType.Contacts:
              {
                var vm = new ContactsViewModel();
                return new Contacts.ContactsView(vm);

                //return new TabView("Contacts", new Page[]
                //  {
                //    new Contacts.ContactsView(vm),
                //    new Contacts.ContactsMapView(vm)
                //  }, vm);   
              }
          }
            
          throw new NotImplementedException("Unknown menu option: " + option.ToString());
        }
    }
}
