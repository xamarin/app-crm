using XamarinCRM.Localization;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;

namespace XamarinCRM.Pages.Products
{
    public class CategoryListPage : BaseProductPage
    {
        readonly string _CategoryId;

        public CategoriesViewModel ViewModel
        {
            get { return BindingContext as CategoriesViewModel; }
        }

        public CategoryListPage(string categoryId = null, string title = null, bool isPerformingProductSelection = false) : base(isPerformingProductSelection)
        {
            _CategoryId = categoryId;

            if (_CategoryId == null)
                Title = TextResources.Products;

            if (title != null)
                Title = title;

            StackLayout stackLayout = new UnspacedStackLayout();

            BindingContext = new CategoriesViewModel(_CategoryId);

            CategoryListView categoryListView = new CategoryListView();
            categoryListView.SetBinding(CategoryListView.ItemsSourceProperty, "Categories");
            categoryListView.SetBinding(CategoryListView.IsEnabledProperty, "IsModelLoaded");
            categoryListView.SetBinding(CategoryListView.IsVisibleProperty, "IsModelLoaded");

            categoryListView.ItemTapped += (sender, e) =>
                {
                    CatalogCategory catalogCategory = ((CatalogCategory)e.Item);

                    if (catalogCategory.HasSubCategories)
                    {
                        Navigation.PushAsync(new CategoryListPage(catalogCategory.Id, catalogCategory.Name, isPerformingProductSelection));
                    }
                    else
                    {
                        Navigation.PushAsync(new ProductListPage(catalogCategory.Id, catalogCategory.Name, isPerformingProductSelection));
                    }
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

