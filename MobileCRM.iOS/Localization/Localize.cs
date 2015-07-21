using System;
using MobileCRM.Localization;
using Xamarin.Forms;
using MonoTouch.Foundation;

[assembly:Dependency(typeof(MobileCRM.iOS.Localize))]

namespace MobileCRM.iOS
{
    public class Localize : ILocalize
    {
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
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
            return new System.Globalization.CultureInfo(netLanguage);
        }
    }
}

