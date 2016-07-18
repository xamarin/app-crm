
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ProductDetailViewModel(Product catalogProduct, bool isPerformingProductSelection = false)
        {
            CatalogProduct = catalogProduct;

            _IsPerformingProductSelection = isPerformingProductSelection;

            AddToOrderCommand = new Command(async () =>
                {
                    MessagingCenter.Send(CatalogProduct, MessagingServiceConstants.UPDATE_ORDER_PRODUCT);
                    await Navigation.PopModalAsync();
                });
        }

        readonly bool _IsPerformingProductSelection;
        public bool IsPerformingProductSelection
        {
            get { return _IsPerformingProductSelection; }
        }

        public ICommand AddToOrderCommand { protected set; get; }

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

