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
using System.Collections.ObjectModel;
using XamarinCRM.Clients;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using XamarinCRM.Models;

namespace XamarinCRM.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        readonly IDataClient _DataClient;

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

        public ProductsViewModel(string categoryId = null)
        {
            _CategoryId = categoryId;

            _Products = new ObservableCollection<Product>();

            _DataClient = DependencyService.Get<IDataClient>();

        }

        Command _LoadProductsCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadProductsCommand
        {
            get
            {
                return _LoadProductsCommand ??
                    (_LoadProductsCommand = new Command(ExecuteLoadProductsCommand));
            }
        }

        async void ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadProductsCommand.ChangeCanExecute();

            Products = new ObservableCollection<Product>((await _DataClient.GetProductsAsync(_CategoryId)));

            IsBusy = false;
            LoadProductsCommand.ChangeCanExecute();
        }
    }
}

