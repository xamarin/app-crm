using XamarinCRM.Models;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;

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
            await App.ExecuteIfConnected(async () =>
                {
                    CatalogProduct catalogProduct = ((CatalogProduct)e.Item);
                        await Navigation.PushAsync(new ProductDetailPage(catalogProduct, isPerformingProductSelection));
                });

            productListView.SetBinding(CategoryListView.HeaderProperty, ".");
            productListView.HeaderTemplate = new DataTemplate(() => {
                Label loadingLabel = new Label()
                    {
                        Text = TextResources.Products_ProductList_LoadingLabel,
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        XAlign = TextAlignment.Center,
                        YAlign = TextAlignment.End,
                        TextColor = Palette._007
                    };
                loadingLabel.SetBinding(Label.IsEnabledProperty, "IsBusy", mode: BindingMode.OneWay);
                loadingLabel.SetBinding(Label.IsVisibleProperty, "IsBusy", mode: BindingMode.OneWay);
                return loadingLabel;
            });
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


