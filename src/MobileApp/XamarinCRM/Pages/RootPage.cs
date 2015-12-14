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
using XamarinCRM.ViewModels;

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

        void SetDetailIfNull(Page page)
        {
            if (Detail == null && page != null)
                Detail = page;
        }

        public async Task NavigateAsync(MenuType id)
        {
            Page newPage;
            if (!Pages.ContainsKey(id))
            {
                switch (id)
                {
                    case MenuType.Sales:
                        var page = new CRMNavigationPage(new SalesDashboardPage
                            { 
                                Title = TextResources.MainTabs_Sales, 
                                Icon = new FileImageSource { File = "sales.png" }
                            });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case MenuType.Customers:
                        page = new CRMNavigationPage(new CustomersPage
                            { 
                                BindingContext = new CustomersViewModel() { Navigation = this.Navigation }, 
                                Title = TextResources.MainTabs_Customers, 
                                Icon = new FileImageSource { File = "customers.png" } 
                            });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case MenuType.Products:
                        page = new CRMNavigationPage(new CategoryListPage
                            { 
                                BindingContext = new CategoriesViewModel() { Navigation = this.Navigation }, 
                                Title = TextResources.MainTabs_Products, 
                                Icon = new FileImageSource { File = "products.png" } 
                            });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
                        break;
                    case MenuType.About:
                        page = new CRMNavigationPage(new AboutItemListPage
                            { 
                                Title = TextResources.MainTabs_Products, 
                                Icon = new FileImageSource { File = "about.png" },
                                BindingContext = new AboutItemListViewModel() { Navigation = this.Navigation }
                            });
                        SetDetailIfNull(page);
                        Pages.Add(id, page);
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
                        BindingContext = new CustomersViewModel() { Navigation = this.Navigation }, 
                        Title = TextResources.MainTabs_Customers, 
                        Icon = new FileImageSource { File = "customers.png" } 
                    })
                {  
                    Title = TextResources.MainTabs_Customers, 
                    Icon = new FileImageSource { File = "customers.png" } 
                });
            Children.Add(new CRMNavigationPage(new CategoryListPage
                    { 
                        BindingContext = new CategoriesViewModel() { Navigation = this.Navigation }, 
                        Title = TextResources.MainTabs_Products, 
                        Icon = new FileImageSource { File = "products.png" } 
                    })
                { 
                    Title = TextResources.MainTabs_Products, 
                    Icon = new FileImageSource { File = "products.png" },
                });
            Children.Add(new CRMNavigationPage(new AboutItemListPage
                    { 
                        Title = TextResources.MainTabs_About, 
                        Icon = new FileImageSource { File = "about.png" },
                        BindingContext = new AboutItemListViewModel() { Navigation = this.Navigation }
                    })
                { 
                    Title = TextResources.MainTabs_About, 
                    Icon = new FileImageSource { File = "about.png" } 
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

