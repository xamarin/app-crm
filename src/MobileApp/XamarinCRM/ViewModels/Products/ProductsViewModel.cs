using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
                    (_LoadProductsCommand = new Command(async () =>
                        await ExecuteLoadProductsCommand()));
            }
        }

        async Task ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Products.Clear();
            IEnumerable<CatalogProduct> products = await _CatalogClient.GetProductsAsync(_CategoryId);
            foreach (var product in products)
                Products.Add(product);

            IsBusy = false;
        }
    }
}

