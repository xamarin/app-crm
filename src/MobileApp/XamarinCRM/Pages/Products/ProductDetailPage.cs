//
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using Xamarin;
using XamarinCRM.Statics;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Products
{
    public class ProductDetailPage : ContentPage
    {
        readonly Product _CatalogProduct;

        public ProductDetailPage(Product catalogProduct, bool isPerformingProductSelection = false)
        {
            _CatalogProduct = catalogProduct;

            Title = _CatalogProduct.Name;

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
            Content = new ScrollView()
            {
                Content = new UnspacedStackLayout()
                {
                    Children =
                    {
                        image, 
                        detailRibbon,
                        descriptionView
                    }
                }
            };
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Insights.Track(InsightsReportingConstants.PAGE_PRODUCTDETAIL);
        }
    }
}

