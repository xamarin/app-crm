using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;
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
        }
    }
}


