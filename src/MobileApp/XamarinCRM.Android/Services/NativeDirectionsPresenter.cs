using System;
using XamarinCRM.Interfaces;
using Xamarin.Forms.Maps;
using Android.Content;
using XamarinCRMAndroid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeDirectionsPresenter))]

namespace XamarinCRMAndroid.Services
{
    public class NativeDirectionsPresenter : INativeDirectionsPresenter
    {
        #region INativeMap implementation
        public void PresentDirections(Pin pin)
        {
            const string mapsApiFormat = "http://maps.google.com/maps?saddr=Current Location&daddr={0}";

            string mapsApiUrl = String.Format(mapsApiFormat, pin.Address);

            string spaceEncodedMapsApiUrl = mapsApiUrl.Replace(" ", "%20");

            var geoUri = Android.Net.Uri.Parse (spaceEncodedMapsApiUrl);

            var mapIntent = new Intent (Intent.ActionView, geoUri);

            mapIntent.SetFlags(ActivityFlags.NewTask);

            Android.App.Application.Context.StartActivity(mapIntent);
        }
        #endregion
        
    }
}

