using Xamarin.Forms;
using XamarinCRM.Statics;

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

