//
//  Copyright 2015  jamesmontemagno
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms.Xaml;



namespace XamarinCRM.Pages
{
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {
            this.root = root;
            InitializeComponent();
            BindingContext = new BaseViewModel(Navigation)
                {
                    Title = "XamarinCRM",
                    Subtitle="XamarinCRM",
                    Icon = "slideout.png"
                };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem { Title = "Sales", MenuType = MenuType.Sales, Icon ="sales.png" },
                    new HomeMenuItem { Title = "Customers", MenuType = MenuType.Customers, Icon = "customers.png" },
                    new HomeMenuItem { Title = "Products", MenuType = MenuType.Products, Icon = "products.png" },
                    new HomeMenuItem { Title = "About", MenuType = MenuType.About, Icon = "about.png" },

                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) => 
                {
                    if(ListViewMenu.SelectedItem == null)
                        return;

                    await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
                };
        }
    }
}

