using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Cells
{
    public class ProductListItemCell : ViewCell
    {
        public ProductListItemCell()
        {
            #region productThumbnailImage
            Image productThumbnailImage = new Image()
            {
                Aspect = Aspect.AspectFit
            };
            productThumbnailImage.SetBinding(Image.SourceProperty, new Binding("ImageUrl"));
            #endregion

            #region image loading indicator
            ActivityIndicator imageLoadingIndicator = new ActivityIndicator()
            {
                BindingContext = productThumbnailImage
            };
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            #endregion

            #region categoryNameLabel
            Label categoryNameLabel = new Label()
            { 
                TextColor = Palette._006,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Small, typeof(Label)), 
                    Android: Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))) * 1.2,
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            // The simple form of the Binding constructor.
            categoryNameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
            #endregion

            #region categoryDescriptionLabel
            Label categoryDescriptionLabel = new Label()
            { 
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            // The simple form of the Binding constructor.
            categoryDescriptionLabel.SetBinding(Label.TextProperty, new Binding("Description"));
            #endregion

            // A ContentView, which will serve as the "top-level" of the cell's view hierarchy. 
            // It also allows a Padding to be set; something that can't be done with a plain View.
            var contentView = new ContentView();

            // set the padding of the contentView
            Thickness padding = new Thickness(10, 10);
            contentView.Padding = padding;

            // A container for the "top-level" of the cell's view hierarchy.
            RelativeLayout relativeLayout = new RelativeLayout()
            {
                BackgroundColor = Color.Transparent
            };

            relativeLayout.Children.Add(
                view: productThumbnailImage,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            relativeLayout.Children.Add(view: imageLoadingIndicator,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            relativeLayout.Children.Add(
                view: categoryNameLabel, 
                xConstraint: Constraint.RelativeToView(productThumbnailImage, (parent, siblingView) => siblingView.X + siblingView.Width + padding.HorizontalThickness / 2),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToView(productThumbnailImage, (parent, siblingView) => parent.Width - siblingView.Width - padding.HorizontalThickness / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            relativeLayout.Children.Add(
                view: categoryDescriptionLabel,
                xConstraint: Constraint.RelativeToView(productThumbnailImage, (parent, siblingView) => siblingView.X + siblingView.Width + padding.HorizontalThickness / 2),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToView(productThumbnailImage, (parent, siblingView) => parent.Width - siblingView.Width - padding.HorizontalThickness / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            // Assign the relativeLayout to Content of contentView
            // This lets us take advantage of ContentView's padding
            contentView.Content = relativeLayout;

            // assign contentView to the View property
            View = contentView;
        }
    }
}

