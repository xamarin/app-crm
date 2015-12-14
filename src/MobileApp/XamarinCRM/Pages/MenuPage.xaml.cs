// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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

