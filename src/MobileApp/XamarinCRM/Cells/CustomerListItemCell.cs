using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Cells
{
    public class CustomerListItemCell : ViewCell
    {
        public Label CompanyNameLabel { get; private set; }

        public Label ContactName { get; private set; }

        public CustomerListItemCell()
        {
            #region company label
            CompanyNameLabel = new Label()
            {
                TextColor = Palette._006,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) * 1.2,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation,
//                BackgroundColor = Color.Red
            };
            CompanyNameLabel.SetBinding(
                Label.TextProperty,
                new Binding("Company"));
            #endregion

            #region contact label
            ContactName = new Label()
            {
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Start,
//                BackgroundColor = Color.Blue
            };

            ContactName.SetBinding(
                Label.TextProperty,
                new Binding("DisplayContact"));
            #endregion

            // A ContentView, which will serve as the "top-level" of the cell's view hierarchy. 
            // It also allows a Padding to be set; something that can't be done with a plain View.
            var contentView = new ContentView();

            // set the padding of the contentView
            contentView.Padding = new Thickness(20, 0);

            // A container for the "top-level" of the cell's view hierarchy.
            RelativeLayout relativeLayout = new RelativeLayout();

            // add the companyNameLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: CompanyNameLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            // add the percentCopleteLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: ContactName,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            // Assign the relativeLayout to Content of contentView
            // This lets us take advantage of ContentView's padding.
            contentView.Content = relativeLayout;

            // assign contentView to the View property
            View = contentView;
        }
    }
}

