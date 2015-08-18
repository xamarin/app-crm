using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;

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

            BindingContext = new CategoriesViewModel(_CategoryId);

            #region category list
            CategoryListView categoryListView = new CategoryListView();
            categoryListView.SetBinding(CategoryListView.ItemsSourceProperty, "Categories");
            categoryListView.SetBinding(CategoryListView.IsEnabledProperty, "IsBusy", converter: new InverseBooleanConverter());
            categoryListView.SetBinding(CategoryListView.IsVisibleProperty, "IsBusy", converter: new InverseBooleanConverter());

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
            #endregion

            #region activity indicator
            ActivityIndicator activityIndicator = new ActivityIndicator()
                {
                    HeightRequest = Sizes.LargeRowHeight
                };

            activityIndicator.BindingContext = ViewModel;
            activityIndicator.SetBinding(IsEnabledProperty, "IsBusy");
            activityIndicator.SetBinding(IsVisibleProperty, "IsBusy");
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            #endregion

            #region loading label
            Label loadingLabel = new Label()
                {
                    Text = TextResources.Products_CategoryList_LoadingLabel,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    HeightRequest = Sizes.MediumRowHeight,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.End,
                    TextColor = Palette._007
                };
            loadingLabel.SetBinding(IsEnabledProperty, "IsBusy");
            loadingLabel.SetBinding(IsVisibleProperty, "IsBusy");
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout();
            stackLayout.Children.Add(loadingLabel);
            stackLayout.Children.Add(activityIndicator);
            stackLayout.Children.Add(categoryListView);
            #endregion

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

