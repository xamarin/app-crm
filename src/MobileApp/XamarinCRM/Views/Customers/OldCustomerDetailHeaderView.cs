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
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

namespace XamarinCRM.Views.Customers
{
//    public class OldCustomerDetailHeaderView : ModelBoundContentView<CustomerDetailViewModel>
//    {
//        public OldCustomerDetailHeaderView()
//        {
//            #region company image
//            Image companyImage = new Image() { Aspect = Aspect.AspectFill };
//            companyImage.SetBinding(Image.SourceProperty, "Account.ImageUrl");
//            #endregion
//
//            #region gradient image
//            Image gradientImage = new Image() { Aspect = Aspect.Fill, Source = new FileImageSource() { File = "bottom_up_gradient" }, HeightRequest = 75, BindingContext = companyImage };
//            gradientImage.SetBinding(Image.IsVisibleProperty, "IsLoading", converter: new InverseBooleanConverter());
//            #endregion
//
//            #region activity indicator
//            ActivityIndicator imageLoadingIndicator = new ActivityIndicator() { BindingContext = companyImage };
//            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
//            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property
//            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property
//            #endregion
//
//            #region company label
//            Label companyLabel = new Label()
//            { 
//                TextColor = Color.White, 
//                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
//                LineBreakMode = LineBreakMode.TailTruncation
//            };
//            companyLabel.SetBinding(
//                Label.TextProperty, 
//                "Account.Company");
//
//            companyLabel.SetBinding(
//                Label.TextColorProperty, 
//                new Binding(
//                    "IsLoading", 
//                    source: companyImage, 
//                    converter: new CompanyLabelBooleanToColorConverter()));
//            #endregion
//
//            #region industry label
//            Label industryLabel = new Label()
//            { 
//                TextColor = Palette._008,
//                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
//                LineBreakMode = LineBreakMode.TailTruncation
//            };
//            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");
//            industryLabel.SetBinding(Label.TextColorProperty, new Binding("IsLoading", source: companyImage, converter: new IndustryLabelBooleanToColorConverter()));
//            #endregion
//
//            #region compose view hierarchy
//            StackLayout stackLayout = new StackLayout()
//            {
//                Spacing = 0,
//                Children =
//                {
//                    companyLabel,
//                    industryLabel
//                },
//                Padding = new Thickness(20) 
//            };
//            AbsoluteLayout absoluteLayout = new AbsoluteLayout() { HeightRequest = 150 };
//            absoluteLayout.Children.Add(companyImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
//            absoluteLayout.Children.Add(imageLoadingIndicator, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
//            absoluteLayout.Children.Add(gradientImage, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
//            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
//            #endregion
//
//            Content = absoluteLayout;
//        }
//    }
}

