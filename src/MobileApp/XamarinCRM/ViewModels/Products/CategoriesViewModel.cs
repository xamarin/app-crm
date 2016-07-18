
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

