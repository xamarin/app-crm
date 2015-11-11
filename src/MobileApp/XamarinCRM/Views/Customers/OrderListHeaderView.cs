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
using XamarinCRM.Statics;
using XamarinCRM.Views.Custom;

namespace XamarinCRM.Views.Customers
{
    public class OrderListHeaderView : ContentView
    {
        public Image AddNewOrderImage { get; private set; }

        public Label AddNewOrderTextLabel { get; private set; }

        public OrderListHeaderView()
        {
            HeightRequest = Sizes.LargeRowHeight;

            #region add new order image
            AddNewOrderImage = new Image()
            {
                Aspect = Aspect.AspectFit
            };
            Device.OnPlatform(
                iOS: () => AddNewOrderImage.Source = new FileImageSource(){ File = "add_ios_blue" }, 
                Android: () => AddNewOrderImage.Source = new FileImageSource() { File = "add_android_blue" }
            );

            AddNewOrderImage.IsVisible = Device.OS != TargetPlatform.Android;
            #endregion

            #region add new order label
            AddNewOrderTextLabel = new Label
            {
                Text = TextResources.Customers_Orders_NewOrder.ToUpper(),
                TextColor = Palette._004,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center,
            };
            #endregion

            #region compose view hierarchy
            BoxView bottomBorder = new BoxView() { BackgroundColor = Palette._013, HeightRequest = 1 };

            const double imagePaddingPercent = .35;

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: AddNewOrderImage,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                xConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)));

            relativeLayout.Children.Add(
                view: AddNewOrderTextLabel,
                xConstraint: Constraint.RelativeToView(AddNewOrderImage, (parent, view) => view.X + (view.Width / 2) + parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToView(AddNewOrderImage, (parent, view) => parent.Width - view.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            relativeLayout.Children.Add(
                view: bottomBorder,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height - 1),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(1)
            );
            #endregion

           
            Content = relativeLayout;

        }
    }
}

