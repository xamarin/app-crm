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
using XamarinCRM.Layouts;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using Xamarin;
using XamarinCRM.Models;

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

            SetBinding(Page.TitleProperty, new Binding("Category", converter: new CategoryTitleConverter(Title)));

            #region category list
            CategoryListView categoryListView = new CategoryListView();
            categoryListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "SubCategories");
            categoryListView.IsPullToRefreshEnabled = true;
            categoryListView.SetBinding(ListView.RefreshCommandProperty, "LoadCategoriesCommand");
            categoryListView.SetBinding(ListView.IsRefreshingProperty, "IsBusy", mode: BindingMode.OneWay);

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

