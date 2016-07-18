
using Xamarin.Forms;

namespace XamarinCRM.Cells
{
    public class PickerCell : ViewCell
    {
        public Picker Picker { get; private set; }

        public Label Label { get; private set; }

        public ContentView PickerWrapper { get; private set; }

        public ContentView LabelWrapper { get; private set; }

        public PickerCell()
        {
            PickerWrapper = new ContentView()
            {
                Content = Picker = new Picker()
                { 
                    HorizontalOptions = LayoutOptions.FillAndExpand
                },
                Padding = new Thickness(0, 0, 10, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            LabelWrapper = new ContentView()
            { 
                Content = Label = new Label()
                {
                    VerticalTextAlignment = TextAlignment.Center,
                }, 
                Padding = new Thickness(15, 0, 0, 0)
            };

            Device.OnPlatform(
                iOS: () =>
                {
                    Label.WidthRequest = 75;
                }
            );

            View = new StackLayout()
            { 
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    LabelWrapper,
                    PickerWrapper
                }
            };
        }
    }
}

