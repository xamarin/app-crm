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
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;
using Xamarin;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Products
{
    public class ProductListPage : ModelBoundContentPage<ProductsViewModel>
    {
        public ProductListPage(string title, bool isPerformingProductSelection = false)
        {
            Title = title;

            #region product list
            ProductListView productListView = new ProductListView();
            productListView.SetBinding(ProductListView.ItemsSourceProperty, "Products");
            productListView.IsPullToRefreshEnabled = true;
            productListView.SetBinding(CategoryListView.RefreshCommandProperty, "LoadProductsCommand");
            productListView.SetBinding(CategoryListView.IsRefreshingProperty, "IsBusy", mode: BindingMode.OneWay);

            productListView.ItemTapped += async (sender, e) =>
            {
                Product catalogProduct = ((Product)e.Item);
                await Navigation.PushAsync(new ProductDetailPage(catalogProduct, isPerformingProductSelection));
            };
            #endregion

            #region compase view hierarchy
            Content = new UnspacedStackLayout()
            {
                Children =
                {
                    productListView
                }
            };
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;
            
            ViewModel.LoadProductsCommand.Execute(ViewModel.CategoryId);

            ViewModel.IsInitialized = true;

            Insights.Track(InsightsReportingConstants.PAGE_PRODUCTLIST);
        }
    }
}


