
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms.Maps;
using XamarinCRM.Models;

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

