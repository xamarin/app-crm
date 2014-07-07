using MobileCRM.CustomControls;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.Pages.Accounts;
using MobileCRM.Shared.Pages.Base;
using MobileCRM.Shared.Pages.Contacts;
using MobileCRM.Shared.Pages.Leads;
using MobileCRM.Shared.ViewModels;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using MobileCRM.Shared.ViewModels.Leads;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages
{
  public class HomeView : MasterDetailPage
  {
    private HomeViewModel ViewModel
    {
      get { return BindingContext as HomeViewModel; }
    }
    HomeMasterView master;
    private Dictionary<MenuType, NavigationPage> pages;
    public HomeView()
    {
      pages = new Dictionary<MenuType, NavigationPage>();
      BindingContext = new HomeViewModel();

      Master = master = new HomeMasterView(ViewModel);

      var homeNav = new NavigationPage(master.PageSelection)
      {
        Tint = Helpers.Color.Blue.ToFormsColor()
      };
      Detail = homeNav;

      pages.Add(MenuType.Accounts, homeNav);

      master.PageSelectionChanged = (menuType) =>
      {

        NavigationPage newPage;
        if (pages.ContainsKey(menuType))
        {
          newPage = pages[menuType];
        }
        else
        {
          newPage = new NavigationPage(master.PageSelection)
          {
            Tint = Helpers.Color.Blue.ToFormsColor()
          };
          pages.Add(menuType, newPage);
        }
        Detail = newPage;
        Detail.Title = master.PageSelection.Title;
        IsPresented = false;
      };

      this.Icon = "slideout.png";
    }
  }


  public class HomeMasterView : BaseView
  {
    public Action<MenuType> PageSelectionChanged;
    private Page pageSelection;
    private MenuType menuType = MenuType.Accounts;
    public Page PageSelection
    {
      get { return pageSelection; }
      set
      {
        pageSelection = value;
        if (PageSelectionChanged != null)
          PageSelectionChanged(menuType);
      }
    }

    Page accounts, contacts, leads;
    public HomeMasterView(HomeViewModel viewModel)
    {
      this.Icon = "slideout.png";
      BindingContext = viewModel;

      BackgroundColor = Helpers.Color.DarkGray.ToFormsColor();

      var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

      var label = new ContentView
      {
        Padding = new Thickness(10, 36, 0, 5),
        Content = new Xamarin.Forms.Label
        {
          TextColor = Helpers.Color.LightGray.ToFormsColor(),
          Text = "MENU",
        }
      };

      Device.OnPlatform(
          iOS: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Micro),
          Android: () => ((Xamarin.Forms.Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Medium)
      );

      layout.Children.Add(label);

      var menu = new ListView
      {
        ItemsSource = viewModel.MenuItems,
        VerticalOptions = LayoutOptions.FillAndExpand,
        BackgroundColor = Color.Transparent,
      };

      var cell = new DataTemplate(typeof(MenuCell));
      cell.SetBinding(TextCell.TextProperty, BaseViewModel.TitlePropertyName);
      cell.SetBinding(ImageCell.ImageSourceProperty, BaseViewModel.IconPropertyName);
      cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

      menu.ItemTemplate = cell;

      menu.ItemSelected += (sender, args) =>
        {

          var menuItem = args.SelectedItem as MenuItem;
          if (menuItem == null)
            return;

          menuType = menuItem.MenuType;
          SetCurrentPage();
        };

      menuType = MenuType.Accounts;
      SetCurrentPage();

      menu.SelectedItem = viewModel.MenuItems[0];
      layout.Children.Add(menu);

      Content = layout;
    }

    private void SetCurrentPage()
    {
      
      switch (menuType)
      {
        case MenuType.Accounts:
          {
            var vm = new AccountsViewModel();
            accounts = new TabView("Accounts", new List<Page>
            {
              new AccountsView(vm),
              new AccountsMapView(vm)
            }, vm);
          }

          PageSelection = accounts;
          break;
        case MenuType.Contacts:
          if (contacts == null)
          {
            var vm = new ContactsViewModel();
            contacts = new TabView("Contacts", new List<Page>
            {
              new ContactsView(vm),
              new ContactsMapView(vm)
            }, vm);
          }

          PageSelection = contacts;
          break;
        case MenuType.Leads:
          if (leads == null)
          {

            var vm = new LeadsViewModel();
            leads = new TabView("Leads", new List<Page>
            {
              new LeadsView(vm),
              new LeadsDashboardView()
            }, vm);
          }

          PageSelection = leads;

          break;
      }
    }
  }
}
