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
using System.Windows.Input;

namespace XamarinCRM.ViewModels.Products
{
    public class CategoriesViewModel : BaseViewModel
    {
        readonly IDataService _DataService;

        readonly bool _IsPerformingProductSelection;
        public bool IsPerformingProductSelection
        {
            get { return _IsPerformingProductSelection; }
        }

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

        public new string Title
        {
            get { return base.Title ?? "Products"; }
            set { base.Title = value; }
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

        public CategoriesViewModel(Category category = null, bool isPerformingProductSelection = false)
        {
            Category = category;

            _IsPerformingProductSelection = isPerformingProductSelection;

            SubCategories = new ObservableCollection<Category>();

            _DataService = DependencyService.Get<IDataService>();

        }

        Command _LoadCategoriesCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadCategoriesCommand
        {
            get { return _LoadCategoriesCommand ?? (_LoadCategoriesCommand = new Command(ExecuteLoadCategoriesCommand)); }
        }

        async void ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadCategoriesCommand.ChangeCanExecute();

            SubCategories = new ObservableCollection<Category>((await _DataService.GetCategoriesAsync((_Category != null) ? _Category.Id : null)));

            IsBusy = false;
            LoadCategoriesCommand.ChangeCanExecute();
        }

        Command _LoadCategoriesRemoteCommand;

        public Command LoadCategoriesRemoteCommand
        {
            get { return _LoadCategoriesRemoteCommand ?? (_LoadCategoriesRemoteCommand = new Command(ExecuteLoadCategoriesRemoteCommand)); }
        }

        async void ExecuteLoadCategoriesRemoteCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadCategoriesRemoteCommand.ChangeCanExecute();

            await _DataService.SynchronizeCategoriesAsync();

            SubCategories = new ObservableCollection<Category>((await _DataService.GetCategoriesAsync((_Category != null) ? _Category.Id : null)));

            IsBusy = false;
            LoadCategoriesRemoteCommand.ChangeCanExecute();
        }
    }
}

