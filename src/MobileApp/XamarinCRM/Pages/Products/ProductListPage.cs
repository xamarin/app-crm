using XamarinCRM.Models;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Converters;
using XamarinCRM.Pages.Base;

namespace XamarinCRM.Pages.Products
{
    public class ProductListPage : ModelBoundContentPage<ProductsViewModel>
    {
        public ProductListPage(string title, bool isPerformingProductSelection = false)
        {
            // hide the navigstion bar on Android
            Device.OnPlatform(Android: () => NavigationPage.SetHasNavigationBar(this, isPerformingProductSelection));

            Title = title;

            #region product list
            ProductListView productListView = new ProductListView();
            productListView.SetBinding(ProductListView.ItemsSourceProperty, "Products");
            productListView.SetBinding(CategoryListView.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            productListView.SetBinding(CategoryListView.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            productListView.ItemTapped += async (sender, e) =>
            await App.ExecuteIfConnected(async () =>
                {
                    CatalogProduct catalogProduct = ((CatalogProduct)e.Item);
                        await Navigation.PushAsync(new ProductDetailPage(catalogProduct, isPerformingProductSelection));
                });
            #endregion

            #region activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator()
            {
                HeightRequest = Sizes.LargeRowHeight
            };
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region loading label
            Label loadingLabel = new Label()
            {
                Text = TextResources.Products_ProductList_LoadingLabel,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HeightRequest = Sizes.MediumRowHeight,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
                TextColor = Palette._007
            };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region compase view hierarchy
            Content = new UnspacedStackLayout()
            {
                Children =
                {
                    loadingLabel,
                    activityIndicator,
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


