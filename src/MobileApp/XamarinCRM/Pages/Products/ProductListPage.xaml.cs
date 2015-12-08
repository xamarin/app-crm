// The MIT License (MIT)
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
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Products;
using Xamarin;
using XamarinCRM.Statics;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Products
{
    public partial class ProductListPage : ProductListPageXaml
    {
        public ProductListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;

            ViewModel.LoadProductsCommand.Execute(ViewModel.CategoryId);

            ViewModel.IsInitialized = true;

            Insights.Track(InsightsReportingConstants.PAGE_PRODUCTLIST);
        }

        async void ProductItemTapped (object sender, ItemTappedEventArgs e)
        {
            Product catalogProduct = ((Product)e.Item);
            await Navigation.PushAsync(new ProductDetailPage() { BindingContext = new ProductDetailViewModel(catalogProduct, ViewModel.IsPerformingProductSelection) { Navigation = ViewModel.Navigation } });
        }
    }

    public abstract class ProductListPageXaml : ModelBoundContentPage<ProductsViewModel> { }
}

