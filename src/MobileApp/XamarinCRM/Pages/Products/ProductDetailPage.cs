using XamarinCRM.Models;
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;

namespace XamarinCRM.Pages.Products
{
    public class ProductDetailPage : BaseProductPage
    {
        readonly CatalogProduct _CatalogProduct;

        public ProductDetailPage(CatalogProduct catalogProduct, bool isPerformingProductSelection = false)
            : base(isPerformingProductSelection)
        {
            _CatalogProduct = catalogProduct;

            Title = catalogProduct.Name;

            #region productImage
            Image image = new Image()
            {
                Source = _CatalogProduct.ImageUrl,
                Aspect = Aspect.AspectFit
            };
            #endregion

            #region ribbonView
            ProductDetailRibbonView detailRibbon = new ProductDetailRibbonView(_CatalogProduct, isPerformingProductSelection);
            #endregion

            #region descriptionView
            ProductDetailDescriptionView descriptionView = new ProductDetailDescriptionView(_CatalogProduct);
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout();
            stackLayout.Children.Add(image);
            stackLayout.Children.Add(detailRibbon);
            stackLayout.Children.Add(descriptionView);
            ScrollView scrollView = new ScrollView() { Content = stackLayout };
            #endregion

            Content = scrollView;
        }
    }
}

