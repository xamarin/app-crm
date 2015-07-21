using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.ViewModels.Products;
using MobileCRM.Views.Products;
using Xamarin.Forms;

namespace MobileCRM.Pages.Products
{
    public class ProductListPage : BaseContentPage
    {
        readonly string _CategoryId;

        public ProductsViewModel ViewModel
        {
            get { return BindingContext as ProductsViewModel; }
        }

        public ProductListPage(string categoryId, string title)
        {
            _CategoryId = categoryId;

            Title = title;

            StackLayout stackLayout = new StackLayout();

            BindingContext = new ProductsViewModel(_CategoryId);

            ProductListView productListView = new ProductListView();
            productListView.ItemsSource = ViewModel.Products;

            productListView.ItemTapped += (sender, e) =>
                {
                    CatalogProduct catalogProduct = ((CatalogProduct)e.Item);

                    Navigation.PushAsync(new ProductDetailPage(catalogProduct));
                };

            ActivityIndicator activityIndicator = new ActivityIndicator()
                {
                    HeightRequest = 60
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


