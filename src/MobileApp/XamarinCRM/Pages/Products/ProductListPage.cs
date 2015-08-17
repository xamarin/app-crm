using XamarinCRM.Models;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.Converters;

namespace XamarinCRM.Pages.Products
{
    public class ProductListPage : BaseProductPage
    {
        readonly string _CategoryId;

        public ProductsViewModel ViewModel
        {
            get { return BindingContext as ProductsViewModel; }
        }

        public ProductListPage(string categoryId, string title, bool isPerformingProductSelection = false) : base(isPerformingProductSelection)
        {
            _CategoryId = categoryId;

            Title = title;

            StackLayout stackLayout = new UnspacedStackLayout();

            BindingContext = new ProductsViewModel(_CategoryId);

            ProductListView productListView = new ProductListView();
            productListView.SetBinding(ProductListView.ItemsSourceProperty, "Products");
            productListView.SetBinding(CategoryListView.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            productListView.SetBinding(CategoryListView.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

            productListView.ItemTapped += (sender, e) =>
                {
                    CatalogProduct catalogProduct = ((CatalogProduct)e.Item);

                    Navigation.PushAsync(new ProductDetailPage(catalogProduct, isPerformingProductSelection));
                };

            ActivityIndicator activityIndicator = new ActivityIndicator()
                {
                    HeightRequest = Sizes.LargeRowHeight
                };

            activityIndicator.BindingContext = ViewModel;
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            stackLayout.Children.Add(activityIndicator);

            stackLayout.Children.Add(productListView);

            Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;
            ViewModel.LoadProductsCommand.Execute(_CategoryId);
            ViewModel.IsInitialized = true;
        }
    }
}


