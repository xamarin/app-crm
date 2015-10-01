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
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.Models;
using Xamarin;

namespace XamarinCRM.Pages.Products
{
    public class CategoryListPage : ModelBoundContentPage<CategoriesViewModel>
    {
        public CategoryListPage(string title = null, bool isPerformingProductSelection = false)
        {
            if (title == null)
            {
                Title = "Products";
            }

            SetBinding(CategoryListPage.TitleProperty, new Binding("Category", converter: new CategoryTitleConverter(Title)));

            #region category list
            CategoryListView categoryListView = new CategoryListView();
            categoryListView.SetBinding(CategoryListView.ItemsSourceProperty, "SubCategories");
            categoryListView.IsPullToRefreshEnabled = true;
            categoryListView.SetBinding(CategoryListView.RefreshCommandProperty, "LoadCategoriesCommand");
            categoryListView.SetBinding(CategoryListView.IsRefreshingProperty, "IsBusy", mode: BindingMode.OneWay);

            categoryListView.ItemTapped += async (sender, e) =>
            {
                Category catalogCategory = ((Category)e.Item);
                if (catalogCategory.HasSubCategories)
                {
                    await Navigation.PushAsync(new CategoryListPage(catalogCategory.Name, isPerformingProductSelection) { BindingContext = new CategoriesViewModel(catalogCategory) });
                }
                else
                {
                    await Navigation.PushAsync(new ProductListPage(catalogCategory.Name, isPerformingProductSelection) { BindingContext = new ProductsViewModel(catalogCategory.Id) });
                }
            };
            #endregion

            #region compose view hierarchy
            Content = new UnspacedStackLayout()
            {
                Children =
                {
                    categoryListView
                }
            };
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;
            
            ViewModel.LoadCategoriesCommand.Execute(ViewModel.Category);

            ViewModel.IsInitialized = true;

            Insights.Track(InsightsReportingConstants.PAGE_CATEGORYLIST);
        }
    }
}

