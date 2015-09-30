//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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


