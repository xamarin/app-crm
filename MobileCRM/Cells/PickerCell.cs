using Xamarin.Forms;

namespace MobileCRM
{
    public class PickerCell : ViewCell
    {
        public Picker Picker { get; private set; }

        public PickerCell()
        {
            Picker = new Picker();

            View = Picker;
        }
    }
}

