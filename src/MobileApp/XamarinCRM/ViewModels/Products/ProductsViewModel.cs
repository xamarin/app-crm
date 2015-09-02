using System.Collections.ObjectModel;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinCRM.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        readonly ICatalogDataClient _CatalogClient;

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

        ObservableCollection<CatalogProduct> _Products;
        public ObservableCollection<CatalogProduct> Products
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

            _Products = new ObservableCollection<CatalogProduct>();

            _CatalogClient = DependencyService.Get<ICatalogDataClient>();

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

            Products = new ObservableCollection<CatalogProduct>((await _CatalogClient.GetProductsAsync(_CategoryId)));

            IsBusy = false;
            LoadProductsCommand.ChangeCanExecute();
        }
    }
}

