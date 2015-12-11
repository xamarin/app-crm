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
using System;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;

namespace XamarinCRM.Statics
{
    public static class Fonts
    {
        public static Font SystemFontAt150PercentOfLarge = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.5);
        public static Font SystemFontAt120PercentOfLarge = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.2);

        public static Font SystemFontAt60PercentOfDefault = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Default, typeof(ChartAxisTitle)) * 0.6);
        public static Font SystemFontAt170PercentOfDefault = Font.SystemFontOfSize(Device.GetNamedSize(NamedSize.Default, typeof(ChartAxisTitle)) * 1.7);
    }
}

