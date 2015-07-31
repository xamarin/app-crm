using MobileCRM.Views.Base;
using Xamarin.Forms;

namespace MobileCRM.Views
{
    public class AndroidTabbedPageHeaderView : BaseTabbedPageHeaderView
    {
        public AndroidTabbedPageHeaderView(string headerTitle, string doneActionTitle)
        {
            // a relative layout allows us to arrange visual elements in relationship to other visual elements, like parent views and sibling views.
            RelativeLayout relativeLayout = new RelativeLayout();

            #region back button
            BackButtonImage = new Image()
            {
                Source = "back_android",
                Aspect = Aspect.AspectFit
            };

            // Padding for the back button image. 
            // The value doesn't actually get applied to any particular Padding property. It just us helps with the math of relatively positioning the back button image.
            const double paddingPercent = .30;

            relativeLayout.Children.Add(
                view: BackButtonImage,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height * paddingPercent),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * paddingPercent * 2)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * paddingPercent * 2))
            );
            #endregion

            #region outerContentView
            ContentView outerContentView = new ContentView()
            {
                BackgroundColor = Palette._002
            };
            #endregion

            #region setup innerContentView
            ContentView innerContentView = new ContentView()
            {
                Padding = new Thickness(10, 0), // give the content some padding on the left and right
                BackgroundColor = Palette._001,
                HeightRequest = Sizes.MediumRowHeight // set the height of the content view
            };
            #endregion

            #region title label
            BackButtonLabel = new Label()
            {
                Text = headerTitle,
                TextColor = Color.White,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Start
            };

            relativeLayout.Children.Add(
                view: BackButtonLabel,
                xConstraint: Constraint.RelativeToView(BackButtonImage, (parent, view) => view.Y + view.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * .60),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
            #endregion

            #region done action title
            DoneActionLabel = new Label()
            {
                Text = doneActionTitle,
                TextColor = Color.White,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.End
            };

            relativeLayout.Children.Add(
                view: DoneActionLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width - (parent.Width * .25)),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => (parent.Width * .25)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
            #endregion

            #region compase the view hierarchy
            innerContentView.Content = relativeLayout;

            outerContentView.Content = innerContentView;

            Content = outerContentView;
            #endregion
        }
    }
}

