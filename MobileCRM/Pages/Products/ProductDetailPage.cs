using MobileCRM.Models;
using MobileCRM.Views.Products;
using Xamarin.Forms;
using MobileCRM.Layouts;

namespace MobileCRM.Pages.Products
{
    public class ProductDetailPage : BaseProductPage
    {
        readonly CatalogProduct _CatalogProduct;

        public ProductDetailPage(CatalogProduct catalogProduct)
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
            ProductDetailRibbonView detailRibbon = new ProductDetailRibbonView(_CatalogProduct);
            #endregion

            #region descriptionView
            ProductDetailDescriptionView descriptionView = new ProductDetailDescriptionView(_CatalogProduct);
            #endregion

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(image);
            stackLayout.Children.Add(detailRibbon);
            stackLayout.Children.Add(descriptionView);

            ScrollView scrollView = new ScrollView();

            scrollView.Content = stackLayout;

            Content = scrollView;
        }
    }
}

