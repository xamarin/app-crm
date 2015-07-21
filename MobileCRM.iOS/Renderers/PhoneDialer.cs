using MobileCRM.iOS.Renderers;
using MobileCRM.Interfaces;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace MobileCRM.iOS.Renderers
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            //Since this is a demo we're not going to dial the actual number.  This is a temporary toll-free number we've set up.
            number = "8555826555";

            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + number));
        }
    }
}