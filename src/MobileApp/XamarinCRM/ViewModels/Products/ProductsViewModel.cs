

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

