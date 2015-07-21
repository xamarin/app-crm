using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileCRM.Clients;
using MobileCRM.Models;

namespace MobileCRM.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        readonly string _CategoryId;

        readonly ICatalogClient _CatalogClient;

        ObservableCollection<CatalogProduct> _Products;
        public ObservableCollection<CatalogProduct> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged("Categories");
            }
        }

        public bool NeedsRefresh { get; set; }

        public ProductsViewModel(string categoryId = null)
        {
            _CategoryId = categoryId;

            _Products = new ObservableCollection<CatalogProduct>();

            _CatalogClient = DependencyService.Get<ICatalogClient>();

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

