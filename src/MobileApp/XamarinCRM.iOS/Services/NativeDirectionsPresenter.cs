using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinCRM.Interfaces;
using XamarinCRM.iOS.Services;

[assembly: Dependency(typeof(NativeDirectionsPresenter))]

namespace XamarinCRM.iOS.Services
{
    public class NativeDirectionsPresenter : INativeDirectionsPresenter
    {
        #region INativeMap implementation
        public void PresentDirections(Pin pin)
        {
            const string mapsApiFormat = "http://maps.apple.com/?saddr=Current Location&daddr={0}";

            string mapsApiUrl = String.Format(mapsApiFormat, pin.Address);

            string spaceEncodedMapsApiUrl = mapsApiUrl.Replace(" ", "%20");

            NSUrl nsUrl = new NSUrl(spaceEncodedMapsApiUrl);

            if (UIApplication.SharedApplication.CanOpenUrl (nsUrl)) 
            {
                UIApplication.SharedApplication.OpenUrl (nsUrl);
            } 
            else 
            {
                new UIAlertView ("Error", "Maps is not supported on this device", null, "Ok").Show();
            }
        }
        #endregion
        
    }
}

