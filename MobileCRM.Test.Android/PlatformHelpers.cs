using System;
using Xamarin.UITest.Android;

namespace MobileCRM.Test
{
    public static class PlatformHelpers
    {
        public static void Drag(this AndroidApp app, float x1, float y1, float x2, float y2) {
            app.DragCoordinates (x1, y1, x2, y2);
        }

        public static void DismissKeyboard(this AndroidApp app) {
            app.Back ();
        }
    }
}

