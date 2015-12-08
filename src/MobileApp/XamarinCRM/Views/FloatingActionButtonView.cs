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

namespace XamarinCRM.Views
{
    public enum FloatingActionButtonSize
    {
        Normal,
        Mini
    }

    public class FloatingActionButtonView : View
    {
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create<FloatingActionButtonView,string>( p => p.ImageName, string.Empty);
        public string ImageName 
        {
            get { return (string)GetValue (ImageNameProperty); } 
            set { SetValue (ImageNameProperty, value); } 
        }

        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create<FloatingActionButtonView,Color>( p => p.ColorNormal, Color.White);
        public Color ColorNormal 
        {
            get { return (Color)GetValue (ColorNormalProperty); } 
            set { SetValue (ColorNormalProperty, value); } 
        }

        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create<FloatingActionButtonView,Color>( p => p.ColorPressed, Color.White);
        public Color ColorPressed 
        {
            get { return (Color)GetValue (ColorPressedProperty); } 
            set { SetValue (ColorPressedProperty, value); } 
        }

        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create<FloatingActionButtonView,Color>( p => p.ColorRipple, Color.White);
        public Color ColorRipple 
        {
            get { return (Color)GetValue (ColorRippleProperty); } 
            set { SetValue (ColorRippleProperty, value); } 
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create<FloatingActionButtonView,FloatingActionButtonSize>( p => p.Size, FloatingActionButtonSize.Normal);
        public FloatingActionButtonSize Size 
        {
            get { return (FloatingActionButtonSize)GetValue (SizeProperty); } 
            set { SetValue (SizeProperty, value); } 
        }

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create<FloatingActionButtonView,bool>( p => p.HasShadow, true);
        public bool HasShadow 
        {
            get { return (bool)GetValue (HasShadowProperty); } 
            set { SetValue (HasShadowProperty, value); } 
        }

        public delegate void ShowHideDelegate(bool animate = true);
        public delegate void AttachToListViewDelegate(ListView listView);

        public ShowHideDelegate Show { get; set; }
        public ShowHideDelegate Hide { get; set; }
        public Action<object, EventArgs> Clicked { get; set; }
    }
}

