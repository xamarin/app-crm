using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class PLAFilamentPage : BasePage
    {
        public PLAFilamentPage(IApp app, Platform platform)
            : base(app, platform, "FIL-PLA-BLU", "PLA Filament")
        {
        }

        public void SelectColor(string color)
        {
            var colorChosen = string.Format("FIL-PLA-{0}", color);
            app.ScrollDownTo(colorChosen);
            app.Tap(colorChosen);
        }
    }
}

