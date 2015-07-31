using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileCRM.Clients;
using MobileCRM.Models;
using Xamarin.Forms;

namespace MobileCRM.ViewModels.Products
{
    public class CategoriesViewModel : BaseViewModel
    {
        readonly string _CategoryId;

        readonly ICatalogClient _CatalogClient;

        ObservableCollection<CatalogCategory> _Categories;
        public ObservableCollection<CatalogCategory> Categories
        {
            get { return _Categories; }
            set
            {
                _Categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public bool NeedsRefresh { get; set; }

        public CategoriesViewModel(string categoryId = null)
        {
            _CategoryId = categoryId;

            _Categories = new ObservableCollection<CatalogCategory>();

            _CatalogClient = DependencyService.Get<ICatalogClient>();

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
                    (_LoadCategoriesCommand = new Command(async () =>
                        await ExecuteLoadCategoriesCommand()));
            }
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsModelLoaded = false;

            Categories.Clear();
            IEnumerable<CatalogCategory> categories = await _CatalogClient.GetCategoriesAsync(_CategoryId);
            foreach (var category in categories)
                Categories.Add(category);

            IsBusy = false;
            IsModelLoaded = true;
        }
    }
}

