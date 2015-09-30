//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using XamarinCRM.Interfaces;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinCRM.iOS.Renderers;

[assembly: Dependency(typeof(PhoneDialer))]

namespace XamarinCRM.iOS.Renderers
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