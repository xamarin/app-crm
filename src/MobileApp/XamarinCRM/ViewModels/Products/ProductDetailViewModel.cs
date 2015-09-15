using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;

namespace XamarinCRM
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ProductDetailViewModel(CatalogProduct catalogProduct)
        {
            CatalogProduct = catalogProduct;
        }

        CatalogProduct _CatalogProduct;

        public CatalogProduct CatalogProduct
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

