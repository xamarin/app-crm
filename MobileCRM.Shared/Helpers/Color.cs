using System;
using Xamarin.Forms;

//#if __IOS__
//using MonoTouch.UIKit;
//using MonoTouch.CoreGraphics;
//#endif

namespace MobileCRM.Shared.Helpers
{
    public static class AppColors
    {
        public static readonly Color BARBKGCOLOR = Color.FromHex("3498DB");

        public static readonly Color CONTENTBKGCOLOR = Color.FromHex("1D2A37");
        public static readonly Color CONTENTLIGHTBKG = Color.FromHex("49698A");
        public static readonly Color DARKBLUEBKG = Color.FromHex("141D26");

        public static readonly Color LABELWHITE = Color.White;
        public static readonly Color LABELGRAY = Color.FromHex("B4BCBC");
        public static readonly Color LABELBLUE = Color.FromHex("46BBE5");

        public static readonly Color SEPARATOR = Color.FromHex("4A4A4A");

    }


    public struct ConvertedColor
    {
        public static readonly ConvertedColor Purple = 0xB455B6;
        public static readonly ConvertedColor Blue = 0x3498DB;
        public static readonly ConvertedColor DarkBlue = 0x2C3E50;
        public static readonly ConvertedColor Green = 0x77D065;
        public static readonly ConvertedColor Gray = 0x738182;
        public static readonly ConvertedColor LightGray = 0xB4BCBC;
        public static readonly ConvertedColor Tan = 0xDAD0C8;
        public static readonly ConvertedColor DarkGray = 0x333333;
        public static readonly ConvertedColor Tint = 0x5AA09B;


        public double R, G, B;

        public static ConvertedColor FromHex(int hex)
        {
            Func<int, int> at = offset => (hex >> offset) & 0xFF;
            return new ConvertedColor
            {
                R = at(16) / 255.0,
                G = at(8) / 255.0,
                B = at(0) / 255.0
            };
        }

        public static implicit operator ConvertedColor(int hex)
        {
            return FromHex(hex);
        }

#if __IOS__
    public UIColor ToUIColor ()
    {
      return UIColor.FromRGB ((float)R, (float)G, (float)B);
    }

    public static implicit operator UIColor (Color color)
    {
      return color.ToUIColor ();
    }

    public static implicit operator CGColor (Color color)
    {
      return color.ToUIColor ().CGColor;
    }
#endif

        //public Xamarin.Forms.Color ToFormsColor()
        //{
        //    return Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
        //}

#if __ANDROID__
        public global::Android.Graphics.Color ToAndroidColor()
        {
          return global::Android.Graphics.Color.Rgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
        }

        public static implicit operator global::Android.Graphics.Color(Color color)
        {
            return color.ToAndroidColor();
        }
#endif
    }



}
