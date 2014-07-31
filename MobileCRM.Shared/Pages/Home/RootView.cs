using MobileCRM.Shared.Models;
using MobileCRM.Shared.Pages.Accounts;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Home;
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

#if WINDOWS_PHONE
            Detail = new ContentPage();
#endif

            Detail = new NavigationPage(displayPage)
            {
              BarBackgroundColor = Helpers.Color.Blue.ToFormsColor(),
              BarTextColor = Color.White
            };

            IsPresented = false;
        }

        Page PageForOption (MenuType option)
        {

          switch (option)
          {
            case MenuType.Dashboard:
              {
                  var vm = new DashboardViewModel() { Navigation = Navigation };

                  return new DashboardView(vm);
              }
            case MenuType.Accounts:
              {
                var vm = new AccountsViewModel() { Navigation = Navigation };
                return new AccountsView(vm);

                //return new TabView("Accounts", new Page[]
                //  {
                //    new AccountsView(vm),
                //    new AccountsMapView(vm)
                //  }, vm);      
              }
            case MenuType.Leads:
              {
                  return new Leads.LeadsView(new ViewModels.Leads.LeadsViewModel() { Navigation = Navigation }); 
              }
            case MenuType.Contacts:
              {
                var vm = new ContactsViewModel();
                return new Contacts.ContactsView(vm); 
              }
          }
            
          throw new NotImplementedException("Unknown menu option: " + option.ToString());
        }
    }
}
