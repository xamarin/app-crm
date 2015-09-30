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
using System.Globalization;
using XamarinCRM.Localization;
using Foundation;
using Xamarin.Forms;
using XamarinCRM.iOS.Localize;

[assembly:Dependency(typeof(Localize))]

namespace XamarinCRM.iOS.Localize
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = pref.Replace("_", "-"); // turns es_ES into es-ES

                if (pref == "pt")
                    pref = "pt-BR"; // get the correct Brazilian language strings from the PCL RESX
                //(note the local iOS folder is still "pt")
            }
            return new CultureInfo(netLanguage);
        }
    }
}

