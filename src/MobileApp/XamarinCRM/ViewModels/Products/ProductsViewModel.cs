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

using System.Collections.ObjectModel;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using XamarinCRM.Models;
using XamarinCRM.Services;

namespace XamarinCRM.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        readonly IDataService _DataService;

        readonly bool _IsPerformingProductSelection;
        public bool IsPerformingProductSelection
        {
            get { return _IsPerformingProductSelection; }
        }

        string _CategoryId;
        public string CategoryId
        {
            get { return _CategoryId; }
            set 
            {
                _CategoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        public new string Title
        {
            get { return base.Title ?? "Products"; }
            set { base.Title = value; }
        }

        ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged("Products");
            }
        }

        public bool NeedsRefresh { get; set; }

        public ProductsViewModel(string categoryId = null, bool isPerformingProductSelection = false)
        {
            _CategoryId = categoryId;

            _IsPerformingProductSelection = isPerformingProductSelection;

            _Products = new ObservableCollection<Product>();

            _DataService = DependencyService.Get<IDataService>();
        }

        Command _LoadProductsCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadProductsCommand
        {
            get { return _LoadProductsCommand ?? (_LoadProductsCommand = new Command(ExecuteLoadProductsCommand)); }
        }

        async void ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadProductsCommand.ChangeCanExecute();

            Products = new ObservableCollection<Product>((await _DataService.GetProductsAsync(_CategoryId)));

            IsBusy = false;
            LoadProductsCommand.ChangeCanExecute();
        }

        Command _LoadProductsRemoteCommand;

        public Command LoadProductsRemoteCommand
        {
            get { return _LoadProductsRemoteCommand ?? (_LoadProductsRemoteCommand = new Command(ExecuteLoadProductsRemoteCommand)); }
        }

        async void ExecuteLoadProductsRemoteCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadProductsRemoteCommand.ChangeCanExecute();

            await _DataService.SynchronizeProductsAsync();

            Products = new ObservableCollection<Product>((await _DataService.GetProductsAsync(_CategoryId)));

            IsBusy = false;
            LoadProductsRemoteCommand.ChangeCanExecute();
        }
    }
}

