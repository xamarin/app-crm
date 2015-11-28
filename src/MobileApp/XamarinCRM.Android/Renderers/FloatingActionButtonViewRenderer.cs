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
using XamarinCRMAndroid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Xamarin.Forms;
using com.refractored.fab;
using Android.Views;
using System.IO;
using XamarinCRM.Views;

[assembly: ExportRenderer (typeof (FloatingActionButtonView), typeof (FloatingActionButtonViewRenderer))]
namespace XamarinCRMAndroid.Renderers
{
    public class FloatingActionButtonViewRenderer : ViewRenderer<FloatingActionButtonView, FrameLayout>
    {
        private const int MARGIN_DIPS = 16;
        private const int FAB_HEIGHT_NORMAL = 56;
        private const int FAB_HEIGHT_MINI = 40;
        private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private readonly Android.Content.Context context;
        private readonly FloatingActionButton fab;

        public FloatingActionButtonViewRenderer()
        {
            context = Xamarin.Forms.Forms.Context;

            float d =  context.Resources.DisplayMetrics.Density;
            var margin = (int)(MARGIN_DIPS * d); // margin in pixels

            fab = new FloatingActionButton(context);
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            lp.LeftMargin = margin;
            lp.TopMargin = margin;
            lp.BottomMargin = margin;
            lp.RightMargin = margin;
            fab.LayoutParameters = lp;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= HandlePropertyChanged;

            if (this.Element != null) {
                //UpdateContent ();
                this.Element.PropertyChanged += HandlePropertyChanged;
            }

            Element.Show = Show;
            Element.Hide = Hide;

            SetFabImage(Element.ImageName);
            SetFabSize(Element.Size);

            fab.ColorNormal = Element.ColorNormal.ToAndroid();
            fab.ColorPressed = Element.ColorPressed.ToAndroid();
            fab.ColorRipple = Element.ColorRipple.ToAndroid();
            fab.HasShadow = Element.HasShadow;
            fab.Click += Fab_Click;

            var frame = new FrameLayout(context);
            frame.RemoveAllViews();
            frame.AddView(fab);

            SetNativeControl (frame);
        }

        public void Show(bool animate = true)
        {
            fab.Show(animate);
        }

        public void Hide(bool animate = true)
        {
            fab.Hide(animate);
        }

        void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content") {
                Tracker.UpdateLayout ();
            } 
            else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName) 
            {
                fab.ColorNormal = Element.ColorNormal.ToAndroid();
            } 
            else if (e.PropertyName == FloatingActionButtonView.ColorPressedProperty.PropertyName) 
            {
                fab.ColorPressed = Element.ColorPressed.ToAndroid();
            } 
            else if (e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName) 
            {
                fab.ColorRipple = Element.ColorRipple.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName) 
            {
                SetFabImage(Element.ImageName);
            }
            else if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName) 
            {
                SetFabSize(Element.Size);
            }
            else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName) 
            {
                fab.HasShadow = Element.HasShadow;
            }
        }

        void SetFabImage(string imageName)
        {
            if(!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName);
                    var resources = context.Resources;
                    var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable", context.PackageName);
                    fab.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeResource(context.Resources, imageResourceName));
                }
                catch(Exception ex)
                {
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
                }
            }
        }

        void SetFabSize(FloatingActionButtonSize size)
        {
            if(size == FloatingActionButtonSize.Mini)
            {
                fab.Size = FabSize.Mini;
                Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
            }
            else
            {
                fab.Size = FabSize.Normal;
                Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
            }
        }

        void Fab_Click (object sender, EventArgs e)
        {
            var clicked = Element.Clicked;
            if(Element != null && clicked != null)
            {
                clicked(sender, e);
            }
        }
    }
}

