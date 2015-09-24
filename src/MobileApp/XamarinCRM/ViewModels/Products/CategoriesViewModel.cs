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

        public CategoriesViewModel(Category category = null)
        {
            Category = category;

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

