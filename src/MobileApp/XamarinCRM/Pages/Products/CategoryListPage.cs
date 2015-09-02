using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Products;
using XamarinCRM.Views.Products;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;

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
            await App.ExecuteIfConnected(async () =>
                {
                    CatalogCategory catalogCategory = ((CatalogCategory)e.Item);
                    if (catalogCategory.HasSubCategories)
                    {
                        await Navigation.PushAsync(new CategoryListPage(catalogCategory.Name, isPerformingProductSelection) { BindingContext = new CategoriesViewModel(catalogCategory) });
                    }
                    else
                    {
                        await Navigation.PushAsync(new ProductListPage(catalogCategory.Name, isPerformingProductSelection) { BindingContext = new ProductsViewModel(catalogCategory.Id) });
                    }
                });
            
            categoryListView.SetBinding(CategoryListView.HeaderProperty, ".");
            categoryListView.HeaderTemplate = new DataTemplate(() => {
                Label loadingLabel = new Label()
                    {
                        Text = TextResources.Products_CategoryList_LoadingLabel,
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
        }
    }
}

