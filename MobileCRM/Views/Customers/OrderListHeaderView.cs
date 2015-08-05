using System;
using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.Layouts;

namespace MobileCRM
{
    public class OrderListHeaderView : ContentView
    {
        public OrderListHeaderView()
        {
            HeightRequest = Sizes.LargeRowHeight;

            RelativeLayout relativeLayout = new RelativeLayout();

            Device.OnPlatform(iOS: () => Padding = new Thickness(0, 20, 0, 0));

            Image addNewOrderImage = new Image()
            {
                Aspect = Aspect.AspectFit
            };
            Device.OnPlatform(
                iOS: () => addNewOrderImage.Source = new FileImageSource(){ File = "add_ios_blue" }, 
                Android: () => addNewOrderImage.Source = new FileImageSource() { File = "add_android_blue" }
            );

            Label addNewOrderTextLabel = new Label
            {
                Text = TextResources.Customers_Orders_NewOrder.ToUpper(),
                TextColor = Palette._006,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center,
            };

            BoxView bottomBorder = new BoxView() { BackgroundColor = Palette._013, HeightRequest = 1 };

            double imagePaddingPercent = .35;

            relativeLayout.Children.Add(
                view: addNewOrderImage,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                xConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)));

            relativeLayout.Children.Add(
                view: addNewOrderTextLabel,
                xConstraint: Constraint.RelativeToView(addNewOrderImage, (parent, view) => view.X + view.Width + parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToView(addNewOrderImage, (parent, view) => parent.Width - view.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            relativeLayout.Children.Add(
                view: bottomBorder,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height - 1),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(1)
            );

            Content = relativeLayout;
        }
    }
}

