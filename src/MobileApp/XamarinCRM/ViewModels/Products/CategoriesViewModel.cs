using System.Collections.ObjectModel;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinCRM.ViewModels.Products
{
    public class CategoriesViewModel : BaseViewModel
    {
        readonly ICatalogDataClient _CatalogClient;

        CatalogCategory _Category;

        public CatalogCategory Category
        {
            get { return _Category; }
            set
            {  
                _Category = value;
                OnPropertyChanged("Category");
            }
        }

        ObservableCollection<CatalogCategory> _SubCategories;

        public ObservableCollection<CatalogCategory> SubCategories
        {
            get { return _SubCategories; }
            set
            {
                _SubCategories = value;
                OnPropertyChanged("SubCategories");
            }
        }

        public bool NeedsRefresh { get; set; }

        public CategoriesViewModel(CatalogCategory category = null)
        {
            Category = category;

            SubCategories = new ObservableCollection<CatalogCategory>();

            _CatalogClient = DependencyService.Get<ICatalogDataClient>();

        }

        Command _LoadCategoriesCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadCategoriesCommand
        {
            get
            {
                return _LoadCategoriesCommand ??
                (_LoadCategoriesCommand = new Command(
                    ExecuteLoadCategoriesCommand));
            }
        }

        async void ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadCategoriesCommand.ChangeCanExecute();

            SubCategories = new ObservableCollection<CatalogCategory>((await _CatalogClient.GetCategoriesAsync((_Category != null) ? _Category.Id : null)));

            IsBusy = false;
            LoadCategoriesCommand.ChangeCanExecute();
        }
    }
}

