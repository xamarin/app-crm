using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class StepperMotorsPage : BasePage
    {
        public StepperMotorsPage(IApp app, Platform platform)
            : base(app, platform, "MOT-12V", "Stepper Motors")
        {
        }

        public void SelectItem(int itemNumber)
        {
            var itemChosen = string.Format("MOT-{0}-V", itemNumber);
            app.ScrollTo(itemChosen);
            app.Tap(itemChosen);
        }
    }
}

