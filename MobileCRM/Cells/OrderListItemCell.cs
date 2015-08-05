using System;
using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.Layouts;
using MobileCRM.Converters;

namespace MobileCRM.Cells
{
    public class OrderListItemCell : ViewCell
    {
        public Label PrimaryLabel { get; private set; }

        public Label SecondaryLabel { get; private set; }

        public Label TernaryLabel { get; private set; }

        public OrderListItemCell()
        {
            #region primary label
            PrimaryLabel = new Label()
            {
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) * 1.2,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation,
                //                BackgroundColor = Color.Red
            };
            PrimaryLabel.SetBinding(
                Label.TextProperty,
                new Binding("Item"));
            #endregion

            #region secondary label
            SecondaryLabel = new Label()
            {
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Start,
                LineBreakMode = LineBreakMode.TailTruncation,
                //                BackgroundColor = Color.Blue
            };

            SecondaryLabel.SetBinding(
                Label.TextProperty,
                "DueDate", 
                converter: new ShortDatePatternConverter());
            #endregion

            #region ternary label
            TernaryLabel = new Label()
                {
                    TextColor = Color.Gray,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    XAlign = TextAlignment.End,
                    YAlign = TextAlignment.Start,
                    //                BackgroundColor = Color.Blue
                };

            TernaryLabel.SetBinding(
                Label.TextProperty,
                "Price", 
                converter: new CurrencyIntegerConverter());
            #endregion

            // A ContentView, which will serve as the "top-level" of the cell's view hierarchy. 
            // It also allows a Padding to be set; something that can't be done with a plain View.
            var contentView = new ContentView();

            // set the padding of the contentView
            contentView.Padding = new Thickness(20, 0);

            BoxView bottomBorder = new BoxView() { BackgroundColor = Palette._013, HeightRequest = 1 };

            // A container for the "top-level" of the cell's view hierarchy.
            RelativeLayout labelsRelativeLayout = new RelativeLayout();

            labelsRelativeLayout.Children.Add(
                view: PrimaryLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            labelsRelativeLayout.Children.Add(
                view: SecondaryLabel,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            labelsRelativeLayout.Children.Add(
                view: TernaryLabel,
                xConstraint: Constraint.RelativeToView(SecondaryLabel, (parent, view) => view.Width),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            RelativeLayout borderRelativeLayout = new RelativeLayout();

            borderRelativeLayout.Children.Add(
                view: bottomBorder,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(1)
            );

            // Assign the relativeLayout to Content of contentView
            // This lets us take advantage of ContentView's padding.
            contentView.Content = labelsRelativeLayout;

            StackLayout stackLayout = new UnspacedStackLayout();

            stackLayout.Children.Add(contentView);

            stackLayout.Children.Add(borderRelativeLayout);

            // assign contentView to the View property
            View = contentView;
        }
    }
}

