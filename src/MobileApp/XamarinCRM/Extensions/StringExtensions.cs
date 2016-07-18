
using Xamarin.Forms;

namespace XamarinCRM
{
    public static class StringExtensions
    {
        public static string CapitalizeForAndroid(this string str)
        {
            return Device.OS == TargetPlatform.Android ? str.ToUpper() : str;
        }
    }
}

