//
//  Copyright 2015  Xamarin Inc.
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
using Xamarin.Forms;
using XamarinCRM.Pages.Customers;
using XamarinCRM.Pages.Products;
using XamarinCRM.Pages.Sales;
using XamarinCRM.Pages.Splash;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.ViewModels.Splash;
using XamarinCRM.Services;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Statics;
using System.Threading.Tasks;
using XamarinCRM.ViewModels.Base;
using System.Collections.Generic;
using XamarinCRM.Pages.About;

namespace XamarinCRM.Pages
{


    public class RootPage : MasterDetailPage
    {
        Dictionary<MenuType, NavigationPage> Pages { get; set; }

        public RootPage()
        {
            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel(Navigation)
            {
                Title = "Xamarin CRM",
                Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(MenuType.Sales);
        }



        public async Task NavigateAsync(MenuType id)
        {
            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case MenuType.Sales:
                        Pages.Add(id, new CRMNavigationPage(new SalesDashboardPage
                                { 
                                    Title = TextResources.MainTabs_Sales, 
                                    Icon = new FileImageSource { File = "sales.png" }
                                }));
                        break;
                    case MenuType.Customers:
                        Pages.Add(id, new CRMNavigationPage(new CustomersPage
                                { 
                                    BindingContext = new CustomersViewModel(Navigation), 
                                    Title = TextResources.MainTabs_Customers, 
                                    Icon = new FileImageSource { File = "customers.png" } 
                                }));
                        break;
                    case MenuType.Products:
                        Pages.Add(id, new CRMNavigationPage(new CategoryListPage
                                { 
                                    BindingContext = new CategoriesViewModel(navigation: Navigation), 
                                    Title = TextResources.MainTabs_Products, 
                                    Icon = new FileImageSource { File = "products.png" } 
                                }));
                        break;
                    case MenuType.About:
                        Pages.Add(id, new CRMNavigationPage(new AboutPage
                                { 
                                    Title = "About", 
                                    Icon = new FileImageSource { File = "about.png" } 
                                }));
                        break;
                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.OS == TargetPlatform.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;

            if (Device.Idiom != TargetIdiom.Tablet)
                IsPresented = false;
        }
    }

    public class RootTabPage : TabbedPage
    {
        public RootTabPage()
        {
            Children.Add(new CRMNavigationPage(new SalesDashboardPage
                    { 
                        Title = TextResources.MainTabs_Sales, 
                        Icon = new FileImageSource { File = "sales.png" }
                    })
                { 
                    Title = TextResources.MainTabs_Sales, 
                    Icon = new FileImageSource { File = "sales.png" }
                });
            Children.Add(new CRMNavigationPage(new CustomersPage
                    { 
                        BindingContext = new CustomersViewModel(Navigation), 
                        Title = TextResources.MainTabs_Customers, 
                        Icon = new FileImageSource { File = "customers.png" } 
                    })
                {  
                    Title = TextResources.MainTabs_Customers, 
                    Icon = new FileImageSource { File = "customers.png" } 
                });
            Children.Add(new CRMNavigationPage(new CategoryListPage
                    { 
                        BindingContext = new CategoriesViewModel(navigation: Navigation), 
                        Title = TextResources.MainTabs_Products, 
                        Icon = new FileImageSource { File = "products.png" } 
                    })
                { 
                    Title = TextResources.MainTabs_Products, 
                    Icon = new FileImageSource { File = "products.png" } 
                });
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            this.Title = this.CurrentPage.Title;
        }
    }

    public class CRMNavigationPage :NavigationPage
    {
        public CRMNavigationPage(Page root)
            : base(root)
        {
            Init();
        }

        public CRMNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Palette._001;
            BarTextColor = Color.White;
        }
    }

    public enum MenuType
    {
        Sales,
        Customers,
        Products,
        About
    }

    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.About;
        }

        public string Icon { get; set; }

        public MenuType MenuType { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public int Id { get; set; }
    }

    /*public class RootPage : TabbedPage
    {
        

        public RootPage()
        {
            
            // the Sales tab page
            this.Children.Add(
                new NavigationPage(new SalesDashboardPage())
                { 
                    Title = TextResources.MainTabs_Sales, 
                    Icon = new FileImageSource() { File = "SalesTab" }
                }
            );

            // the Customers tab page
            this.Children.Add(
                new CustomersPage()
                { 
                    BindingContext = new CustomersViewModel(Navigation), 
                    Title = TextResources.MainTabs_Customers, 
                    Icon = new FileImageSource() { File = "CustomersTab" } 
                }
            );

            // the Products tab page
            this.Children.Add(
                new NavigationPage(new CategoryListPage() { BindingContext = new CategoriesViewModel() } )
                { 
                    Title = TextResources.MainTabs_Products, 
                    Icon = new FileImageSource() { File = "ProductsTab" } 
                }
            );
        }
    }*/
}

