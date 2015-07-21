using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.Views.Products;
using Xamarin.Forms;

namespace MobileCRM.Pages.Products
{
    public class ProductDetailPage : BaseContentPage
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
            ProductDetailRibbon detailRibbon = new ProductDetailRibbon(_CatalogProduct);
            #endregion

            #region descriptionView
            ProductDetailDescriptionView descriptionView = new ProductDetailDescriptionView(_CatalogProduct);
            #endregion

            StackLayout stackLayout = new StackLayout() { Spacing = 0 };

            stackLayout.Children.Add(image);
            stackLayout.Children.Add(detailRibbon);
            stackLayout.Children.Add(descriptionView);

            ScrollView scrollView = new ScrollView();

            scrollView.Content = stackLayout;

            Content = scrollView;
        }
    }
}

