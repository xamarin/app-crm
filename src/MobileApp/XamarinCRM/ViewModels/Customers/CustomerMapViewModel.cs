using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models;
using Xamarin.Forms.Maps;

namespace XamarinCRM
{
    public class CustomerMapViewModel : BaseViewModel
    {
        Geocoder _GeoCoder;

        public CustomerMapViewModel(Account account)
        {


            _GeoCoder = new Geocoder();
        }
    }
}

