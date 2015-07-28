using MobileCRM.Localization;
using MobileCRM.Models;
using MobileCRM.ViewModels.Products;
using MobileCRM.Views.Products;
using Xamarin.Forms;

namespace MobileCRM.Pages.Products
{
    public class CategoryListPage : BaseProductPage
    {
        readonly string _CategoryId;

        public CategoriesViewModel ViewModel
        {
            get { return BindingContext as CategoriesViewModel; }
        }

        public CategoryListPage(string categoryId = null, string title = null)
        {
            _CategoryId = categoryId;

            if (_CategoryId == null)
                Title = TextResources.Products;

            if (title != null)
                Title = title;

            StackLayout stackLayout = new StackLayout();

            BindingContext = new CategoriesViewModel(_CategoryId);

            CategoryListView categoryListView = new CategoryListView();
            categoryListView.SetBinding(CategoryListView.ItemsSourceProperty, "Categories");

            categoryListView.ItemTapped += (sender, e) =>
                {
                    CatalogCategory catalogCategory = ((CatalogCategory)e.Item);

                    if (catalogCategory.HasSubCategories)
                    {
                        Navigation.PushAsync(new CategoryListPage(catalogCategory.Id, catalogCategory.Name));
                    }
                    else
                    {
                        Navigation.PushAsync(new ProductListPage(catalogCategory.Id, catalogCategory.Name));
                    }
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

            stackLayout.Children.Add(categoryListView);

            Content = stackLayout;
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;
            ViewModel.LoadCategoriesCommand.Execute(_CategoryId);
            ViewModel.IsInitialized = true;

        }
    }
}

