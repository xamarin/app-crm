

[assembly: Dependency(typeof(MobileCRM.WindowsPhone.Localize))]

namespace MobileCRM.WindowsPhone
{	
	public class Localize : MobileCRM.ILocalize
    {
        public System.Globalization.CultureInfo GetCurrentCultureInfo ()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }
}