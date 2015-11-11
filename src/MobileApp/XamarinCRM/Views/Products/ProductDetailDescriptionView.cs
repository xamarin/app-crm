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
using XamarinCRM.Models;

namespace XamarinCRM.Views.Products
{
    public class ProductDetailDescriptionView : ContentView
    {
        public ProductDetailDescriptionView(Product catalogProduct)
        {
            Label nameLabel = new Label()
            { 
                Text = catalogProduct.Name,
                TextColor = Device.OnPlatform(Palette._006, Palette._006, Palette._013),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            
            Label descriptionLabel = new Label()
            { 
                Text = catalogProduct.Description,
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))
            };

            Thickness padding = new Thickness(20);

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: nameLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width)
            );

            relativeLayout.Children.Add(
                view: descriptionLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToView(nameLabel, (parent, siblingView) => siblingView.Height + padding.VerticalThickness / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width)
            );

            Padding = padding;

            Content = relativeLayout;
        }
    }
}


