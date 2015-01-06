using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using MobileCRM.iOS;
using MobileCRM.Shared.Interfaces;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

[assembly: Dependency(typeof(PhoneDialer))]
namespace MobileCRM.iOS
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + number));
        }

    }
}