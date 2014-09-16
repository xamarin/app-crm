using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Home
{
    public class MenuView : ContentPage
    {
      public ListView Menu { get; private set; }
      private HomeViewModel viewModel;

      public MenuView()
      {
        this.Icon = "slideout.png";
        this.Title = "menu";
        BindingContext = viewModel = new HomeViewModel();


        this.BackgroundImage = "menubk.png";



        var label = new ContentView
        {
          Padding = new Thickness(10, 36, 0, 5),
          Content = new Xamarin.Forms.Label
          {
            TextColor = Color.White,
            Text = "MENU",
          }
        };

  
        Menu = new ListView
        {
          ItemsSource = viewModel.MenuItems,
          VerticalOptions = LayoutOptions.FillAndExpand,          
          BackgroundColor = Color.Transparent,
        };

        var cell = new DataTemplate(typeof(MenuCell));
        cell.SetBinding(TextCell.TextProperty, BaseViewModel.TitlePropertyName);
        cell.SetBinding(ImageCell.ImageSourceProperty, BaseViewModel.IconPropertyName);
        cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

        Menu.ItemTemplate = cell;


        Content = new StackLayout
        {
          Spacing = 10,
          VerticalOptions = LayoutOptions.FillAndExpand,
          Children = { label, Menu}
        };
      }
    }
}
