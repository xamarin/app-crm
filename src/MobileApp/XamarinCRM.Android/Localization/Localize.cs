
using Xamarin.Forms;
using XamarinCRM.Localization;
using XamarinCRMAndroid.Localization;
using System.Globalization;

[assembly:Dependency(typeof(Localize))]

namespace XamarinCRMAndroid.Localization
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo ()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace ("_", "-"); // turns pt_BR into pt-BR
            return new CultureInfo(netLanguage);
        }
    }
}

