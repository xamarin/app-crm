using System;
using Xamarin.UITest.iOS;

namespace MobileCRM.Test
{
    public static class PlatformHelpers
    {
        public static void Drag(this iOSApp app, float x1, float y1, float x2, float y2) {
            app.PanCoordinates (x1, y1, x2, y2);
        }

        public static void DismissKeyboard(this iOSApp app) {
            app.PressEnter ();
        }
    }
}

