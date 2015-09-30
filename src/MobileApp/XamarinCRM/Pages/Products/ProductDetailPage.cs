//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using XamarinCRM.Views.Products;
using Xamarin.Forms;
using XamarinCRM.Layouts;
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
    }
}

