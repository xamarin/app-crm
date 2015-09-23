using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ProductDetailViewModel(Product catalogProduct)
        {
            CatalogProduct = catalogProduct;
        }

        Product _CatalogProduct;

        public Product CatalogProduct
        {
            get { return _CatalogProduct; }
            set
            {
                _CatalogProduct = value;
                OnPropertyChanged("CatalogProduct");
            }
        }
    }
}

