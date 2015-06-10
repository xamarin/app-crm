using Xamarin.Forms;
using MobileCRM.ViewModels.Home;
using MobileCRM.CustomControls;

namespace MobileCRM.Pages.Home
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; private set; }

        HomeViewModel viewModel;

        public MenuPage()
        {
            this.Icon = "slideout.png";
            this.Title = "☰";
            BindingContext = viewModel = new HomeViewModel();

            this.BackgroundImage = "menubk.png";

            var label = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Xamarin.Forms.Label
                {
                    TextColor = Color.White,
                            Text = "☰",
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
                Children = { label, Menu }
            };
        }
    }
}