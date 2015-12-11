// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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

