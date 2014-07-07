using MobileCRM.CustomControls;
using MobileCRM.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            BindingContext = new LoginViewModel(Navigation);

            BackgroundColor = Helpers.Color.Blue.ToFormsColor();

            var sig = new MobileCRM.CustomControls.SignaturePad()
            {
              HeightRequest = 200
            };  
          
            var layout = new StackLayout { Padding = 30 };
            layout.Children.Add(sig);
            var label = new Label
            {
                Text = "Connect with Your Data",
                Font = Font.BoldSystemFontOfSize(NamedSize.Large),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center, // Center the text in the blue box.
                YAlign = TextAlignment.Center, // Center the text in the blue box.
            };

            layout.Children.Add(label);

            var username = new Entry { Placeholder = "Username" };
            username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
            layout.Children.Add(username);

            var password = new Entry { Placeholder = "Password" };
            password.SetBinding(Entry.TextProperty, LoginViewModel.PasswordPropertyName);
            layout.Children.Add(password);


            
            var button = new Button { Text = "Sign In", TextColor = Color.White };
            button.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            layout.Children.Add(button);

            var items = new List<BarItem>();
            items.Add(new BarItem { Name = "a", Value = 10 });
            items.Add(new BarItem { Name = "b", Value = 15 });
            items.Add(new BarItem { Name = "c", Value = 20 });
            items.Add(new BarItem { Name = "d", Value = 5 });
            items.Add(new BarItem { Name = "e", Value = 14 });
            var chart = new MobileCRM.CustomControls.BarChart
            {
              Items = items,
              HeightRequest = 500
            };
            layout.Children.Add(chart);
            Content = new ScrollView { Content = layout };
        }
    }
}
