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
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;

namespace XamarinCRM.ViewModels.Products
{
    public class CategoriesViewModel : BaseViewModel
    {
        readonly IDataClient _DataClient;

        Category _Category;

        public Category Category
        {
            get { return _Category; }
            set
            {  
                _Category = value;
                OnPropertyChanged("Category");
            }
        }

        ObservableCollection<Category> _SubCategories;

        public ObservableCollection<Category> SubCategories
        {
            get { return _SubCategories; }
            set
            {
                _SubCategories = value;
                OnPropertyChanged("SubCategories");
            }
        }

        public bool NeedsRefresh { get; set; }

        public CategoriesViewModel(Category category = null, INavigation navigation = null)
        {
            Category = category;
            Navigation = navigation;
            SubCategories = new ObservableCollection<Category>();

            _DataClient = DependencyService.Get<IDataClient>();

        }

        Command _LoadCategoriesCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadCategoriesCommand
        {
            get
            {
                return _LoadCategoriesCommand ?? (_LoadCategoriesCommand = new Command(ExecuteLoadCategoriesCommand));
            }
        }

        async void ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadCategoriesCommand.ChangeCanExecute();

            SubCategories = new ObservableCollection<Category>((await _DataClient.GetCategoriesAsync((_Category != null) ? _Category.Id : null)));

            IsBusy = false;
            LoadCategoriesCommand.ChangeCanExecute();
        }
    }
}

